﻿using Microsoft.AspNetCore.Http;

namespace Mix.Lib.Dtos
{
    public class SearchMixDataDto : SearchRequestDto
    {
        public SearchMixDataDto()
        {

        }
        public SearchMixDataDto(SearchRequestDto req, HttpRequest request)
        {
            Culture = req.Culture;
            Keyword = req.Keyword;
            FromDate = req.FromDate;
            ToDate = req.ToDate;
            PageIndex = req.PageIndex;
            PageSize = req.PageSize;
            OrderBy = req.OrderBy;  
            Direction = req.Direction;
            Status = req.Status;
            MixDatabaseName = request.Query[MixRequestQueryKeywords.DatabaseName];
            
            if (int.TryParse(request.Query[MixRequestQueryKeywords.DatabaseId], out int mixDbId))
            {
                MixDatabaseId = mixDbId;
            }
            if (bool.TryParse(request.Query["isGroup"], out bool isGroup))
            {
                IsGroup = isGroup;
            }
            if (Enum.TryParse(request.Query["compareKind"], out MixCompareOperatorKind compareKind))
            {
                 CompareKind = compareKind;
            }
            if (!string.IsNullOrEmpty(request.Query["fields"]))
            {
                Fields = JObject.Parse(request.Query["fields"]);
            }
        }

        public int MixDatabaseId { get; set; }
        public string MixDatabaseName { get; set; }
        public MixCompareOperatorKind CompareKind { get; set; }
        public bool IsGroup { get; set; }
        public JObject Fields { get; set; }

    }
}
