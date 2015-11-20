using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ctrip.SOA.Infratructure.ServiceProxy;
using Ctrip.SOA.Bussiness.User.Service;
using Ctrip.SOA.Bussiness.User.DataContract;
using System.Xml;
using System.Configuration;
using Ctrip.SOA.Infratructure.ServiceProxy;
using System.Collections.Generic;
using System.ServiceModel;

namespace Ctrip.SOA.Business.Test
{
    [TestClass]
    public class UserServiceTest
    {
      

        [TestMethod]
        public void AddUserTest()
        {
            IUserService userService = ServiceProxyFactory.CreateChannel<IUserService>();
            var model = new Bussiness.User.DataContract.UserModelRequest();
            model.UserName = "dddd";
            UserModelResponse response = userService.AddUser(model);
            Assert.IsTrue(response.IsSuccess);

            //Ctrip.SOA.Service.Test.UserService.UserServiceClient client = new Service.Test.UserService.UserServiceClient();
            //var model = new Bussiness.User.DataContract.UserModelRequest();
            //model.UserName = "dddd";
            //UserModelResponse response = client.AddUser(model);
            //Assert.IsTrue(response.IsSuccess);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            try
            {
                IUserService userService = ServiceProxyFactory.CreateChannel<IUserService>();
                var model = new Bussiness.User.DataContract.UserModelRequest();
                model.UserId = 8;
                model.UserName = "44444444444444444444";
                BaseResponse response = userService.UpdateUser(model);
                Assert.IsTrue(response.IsSuccess);
            }
            catch (FaultException ex)
            {
                throw ex;
            }

        }

        [TestMethod]
        public void DeleteUserTest()
        {
            IUserService userService = ServiceProxyFactory.CreateChannel<IUserService>();
            var model = new Bussiness.User.DataContract.UserModelRequest();
            model.UserId = 9;

            BaseResponse response = userService.DeleteUser(model);
            Assert.IsTrue(response.IsSuccess);

        }

        [TestMethod]
        public void GetUserTest()
        {
            IUserService userService = ServiceProxyFactory.CreateChannel<IUserService>();
            var model = new Bussiness.User.DataContract.UserModelRequest();


            List<UserModelResponse> users = userService.GetAllUsers();
            Assert.IsTrue(users.Count>1);

            UserModelResponse user = userService.GetUser(new UserModelRequest { UserId = 1 });
            Assert.IsNotNull(user);

        }
    }
}
