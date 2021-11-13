
namespace Mix.Tenancy.Domain.ViewModels.Init
{
    public class InitCultureViewModel : ViewModelBase<MixCmsContext, MixCulture, int, InitCultureViewModel>
    {
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Icon { get; set; }
        public string Lcid { get; set; }
        public string Specificulture { get; set; }
        public virtual string Image { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string SystemName { get; set; }
        public virtual string Description { get; set; }

        public int MixTenantId { get; set; }

        public override void InitDefaultValues(string language = null, int? cultureId = null)
        {
            CreatedDateTime = DateTime.UtcNow;
            Status = MixContentStatus.Published;
        }
    }
}
