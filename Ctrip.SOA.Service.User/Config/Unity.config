<?xml version="1.0" encoding="utf-8" ?>
<unity xmlns="http://schemas.microsoft.com/practices/2010/unity">
  <container name="Ctrip.SOA.Service.User">
    <register type="Ctrip.SOA.Repository.User.Interface.IUserInsertDB,Ctrip.SOA.Repository.User"
      mapTo="Ctrip.SOA.Repository.User.Dal.UserInsertDB,Ctrip.SOA.Repository.User">
    </register>
    <register type="Ctrip.SOA.Repository.User.Interface.IUserSelectDB,Ctrip.SOA.Repository.User"
       mapTo="Ctrip.SOA.Repository.User.Dal.UserSelectDB,Ctrip.SOA.Repository.User">
    </register>
    <register type="Ctrip.SOA.Repository.User.IRepository.IUserRepository,Ctrip.SOA.Repository.User.IRepository"
          mapTo="Ctrip.SOA.Repository.User.UserRepository,Ctrip.SOA.Repository.User">
    </register>
     <register type="Ctrip.SOA.Bussiness.User.IBusiness.IUserBLL,Ctrip.SOA.Bussiness.User.IBusiness"
         mapTo="Ctrip.SOA.Bussiness.User.UserBLL,Ctrip.SOA.Bussiness.User">
    </register>
    <register type="Ctrip.SOA.Bussiness.User.Service.IUserService,Ctrip.SOA.Bussiness.User"
        mapTo="Ctrip.SOA.Service.User.UserService,Ctrip.SOA.Service.User">
    </register>
  </container>
 
</unity>
