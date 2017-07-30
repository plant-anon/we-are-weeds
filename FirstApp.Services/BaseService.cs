using FirstApp.Data;
using FirstApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Services {
    public abstract class BaseService {
        public UnitOfWork uow;

        public BaseService(UnitOfWork context) => uow = context;
    }
}
