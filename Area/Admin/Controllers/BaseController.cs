using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GameTracker.Data.DataConstants;

namespace GameTracker.Area.Admin.Controllers
{
    [Authorize(Roles = RoleContants.Admin)]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}
