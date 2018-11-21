using Abp.Web.Mvc.Views;

namespace FirstAbpProject.Web.Views
{
    public abstract class FirstAbpProjectWebViewPageBase : FirstAbpProjectWebViewPageBase<dynamic>
    {

    }

    public abstract class FirstAbpProjectWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected FirstAbpProjectWebViewPageBase()
        {
            LocalizationSourceName = FirstAbpProjectConsts.LocalizationSourceName;
        }
    }
}