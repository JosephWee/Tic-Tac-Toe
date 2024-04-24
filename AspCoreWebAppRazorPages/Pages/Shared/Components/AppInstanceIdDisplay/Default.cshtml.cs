using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToe.Cache;

namespace BlazorServerApp.Pages.Shared.Components
{
    public class AppInstanceIdDisplayViewComponent : ViewComponent
    {
        private readonly IDistributedCache _distCache;
        public AppInstanceIdDisplayViewComponent(IDistributedCache distCache)
        {
            _distCache = distCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            WebAppInstanceInfo instanceInfo = new WebAppInstanceInfo();
            instanceInfo.AppInstanceId = _distCache.AppInstanceId();
            instanceInfo.AppStartTimeUTC = _distCache.GetCache("AppStartTimeUTC") ?? string.Empty;

            return View(instanceInfo);
        }
    }
}
