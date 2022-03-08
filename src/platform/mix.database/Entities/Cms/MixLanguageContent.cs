﻿using Mix.Database.Entities.Base;

namespace Mix.Database.Entities.Cms
{
    public class MixLanguageContent : MultiLanguageUniqueNameContentBase<int>
    {
        public string DefaultContent { get; set; }

        public virtual MixLanguage MixLanguage { get; set; }
    }
}
