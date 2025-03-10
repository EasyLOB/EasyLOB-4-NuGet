﻿using EasyLOB;
using EasyLOB.Mvc;

namespace MyLOB.Mvc
{
    public partial class MyLOBTasksController : BaseMvcControllerTask
    {
        #region Properties

        protected IMyLOBUnitOfWork UnitOfWork { get; }

        #endregion Properties

        #region Methods

        public MyLOBTasksController(IMyLOBUnitOfWork unitOfWork,
            IAuthorizationManager authorizationManager)
            : base(authorizationManager)
        {
            UnitOfWork = unitOfWork;
        }

        protected override bool IsValid(ZOperationResult operationResult, IZValidatableObject entity)
        {
            bool result = base.IsValid(operationResult, entity);

            if (!result)
            {
                operationResult.Clear(); // Html.BeginForm() + Html.ValidationSummary()
            }

            return result;
        }

        #endregion Methods
    }
}
 