﻿using Mix.Database.Entities.Cms;
using System;

namespace Mix.Database.Entities.Base
{
    public abstract class MultilanguageContentBase<TPrimaryKey> : EntityBase<TPrimaryKey>
        where TPrimaryKey : IComparable
    {
        public string Specificulture { get; set; }

        public TPrimaryKey ParentId { get; set; }
        public int MixCultureId { get; set; }
        public virtual MixCulture MixCulture { get; set; }
    }
}
