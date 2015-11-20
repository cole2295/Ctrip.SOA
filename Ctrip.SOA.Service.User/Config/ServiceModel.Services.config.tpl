<?xml version="1.0"?>
<services>
  <!--用户服务-->
  <service name="Ctrip.SOA.Service.User.UserService">
    <endpoint address="" binding="basicHttpBinding" contract="Ctrip.SOA.Bussiness.User.Service.IUserService" bindingConfiguration="BindingbasicHttp"/>
    <host>
      <baseAddresses>
        <add baseAddress="{$UserServiceSvcUrl}"/>
      </baseAddresses>
    </host>
  </service>

</services>