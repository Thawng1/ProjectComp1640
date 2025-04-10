using Microsoft.AspNetCore.SignalR;
using ProjectComp1640.Data;

namespace ProjectComp1640.Dashboard
{
    public class DashboardService
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<DashboardHub> _hubContext;
        public DashboardService(ApplicationDBContext context, IHubContext<DashboardHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
    }
}
