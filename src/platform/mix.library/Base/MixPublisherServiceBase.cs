﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mix.Queue.Engines;
using Mix.Queue.Engines.MixQueue;
using Mix.Queue.Models.QueueSetting;

namespace Mix.Lib.Base
{
    public class MixPublisherServiceBase : BackgroundService, IHostedService
    {
        private IQueueService<MessageQueueModel> _queueService;
        private List<IQueuePublisher<MessageQueueModel>> _publishers;
        private IConfiguration _configuration;
        private IWebHostEnvironment _environment;
        private MixMemoryMessageQueue<MessageQueueModel> _queue;
        private const int MAX_CONSUME_LENGTH = 100;
        private string _topicId;

        public MixPublisherServiceBase(
            string topicId,
            IQueueService<MessageQueueModel> queueService,
            IConfiguration configuration, IWebHostEnvironment environment,
            MixMemoryMessageQueue<MessageQueueModel> queue)
        {
            _queueService = queueService;
            _configuration = configuration;
            _environment = environment;
            _queue = queue;
            _topicId = topicId;
        }


        private List<IQueuePublisher<MessageQueueModel>> CreatePublisher(string topicName,
            MixMemoryMessageQueue<MessageQueueModel> queue, CancellationToken cancellationToken)
        {
            try
            {
                List<IQueuePublisher<MessageQueueModel>> queuePublishers =
                    new List<IQueuePublisher<MessageQueueModel>>();
                var providerSetting = _configuration["MessageQueueSetting:Provider"];
                var settingPath = _configuration.GetSection("MessageQueueSetting:GoogleQueueSetting");
                var provider = Enum.Parse<MixQueueProvider>(providerSetting);

                switch (provider)
                {
                    case MixQueueProvider.GOOGLE:

                        var googleSetting = new GoogleQueueSetting();
                        settingPath.Bind(googleSetting);
                        googleSetting.CredentialFile = Path.Combine(_environment.ContentRootPath, googleSetting.CredentialFile);

                        queuePublishers.Add(
                            QueueEngineFactory.CreatePublisher<MessageQueueModel>(
                                provider, googleSetting, topicName));
                        break;
                    case MixQueueProvider.MIX:
                        var mixSetting = new MixQueueSetting();
                        settingPath.Bind(mixSetting);
                        queuePublishers.Add(
                           QueueEngineFactory.CreatePublisher(
                               provider, mixSetting, topicName, queue));

                        break;
                }

                return queuePublishers;
            }
            catch (Exception ex)
            {
                throw new MixException(Heart.Enums.MixErrorStatus.ServerError, ex);
            }
        }

        private Task StartMixQueueEngine(CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var inQueueItems = _queueService.ConsumeQueue(MAX_CONSUME_LENGTH, _topicId);

                    if (inQueueItems.Any() && _publishers != null)
                    {
                        Parallel.ForEach(_publishers, async publisher => { await publisher.SendMessages(inQueueItems); });
                    }
                    await Task.Delay(1000, cancellationToken);
                }
            }, cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var providerSetting = _configuration["MessageQueueSetting:Provider"];
            var provider = Enum.Parse<MixQueueProvider>(providerSetting);
            _publishers = CreatePublisher(_topicId, _queue, stoppingToken);
            if (provider == MixQueueProvider.MIX)
            {
                return StartMixQueueEngine(stoppingToken);
            }
            return Task.CompletedTask;
        }
    }
}
