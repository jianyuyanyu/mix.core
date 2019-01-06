﻿'use strict';
app.factory('ModuleArticleService', ['$rootScope', 'CommonService', 'BaseService',
    function ($rootScope, commonService, baseService) {

        var serviceFactory = Object.create(baseService);
        serviceFactory.init('module-article');
        var _updateInfos = async function (modules) {

            var req = {
                method: 'POST',
                url: this.prefixUrl + '/update-infos',
                data: JSON.stringify(modules)
            };
            return await commonService.getApiResult(req);
        };
        serviceFactory.updateInfos = _updateInfos;
        return serviceFactory;

    }]);
