using Business.Abstract;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace URLMonitoringSvc
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IScheduleService _IScheduleService;
        

        public Worker(ILogger<Worker> logger, IScheduleService scheduleService )
        {
            _logger = logger;
            _IScheduleService = scheduleService;
           
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                _IScheduleService.RunSchedule();
                await Task.Delay(60000, stoppingToken);
            }
        }

    }
}
