//using PostSharp.Aspects;
//using SimpleCloudMonolithic.Application.Common.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;

//namespace SimpleCloud_Monolithic.Application.Common.Aspects
//{
//    public class AddToBillAspect : OnMethodBoundaryAspect
//    {
//        private readonly Stopwatch _timer;
//        private readonly IApplicationDbContext _dbContext;
 
//        public AddToBillAspect(IApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public override void OnEntry(MethodExecutionArgs args)

//        {
//            _timer.Start();
//        }

//        public override void OnExit(MethodExecutionArgs args)

//        {
//            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

//            _timer.Stop();

            
//        }
//    }
//}
