<?xml version="1.0"?>
<bindings >
  <netNamedPipeBinding>
    <binding name="BindingnetNamedPipe" transactionFlow="true" transactionProtocol="OleTransactions">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None" />
    </binding>
  </netNamedPipeBinding>
  <netTcpBinding>
    <binding name="BindingnetTcp" sendTimeout="00:10:00" transactionFlow="true"
      transactionProtocol="OleTransactions" maxReceivedMessageSize="2147483647">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None" />
    </binding>
  </netTcpBinding>
  <wsHttpBinding>
    <binding name="BindingWsHttp" sendTimeout="00:10:00" transactionFlow="true" receiveTimeout="00:10:00"
      maxReceivedMessageSize="2147483647">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None"></security>
      <!--<security mode="Message">
            <message clientCredentialType="UserName"/>
          </security>-->
    </binding>
  </wsHttpBinding>
  <!--<basicHttpBinding>
    <binding name="BindingbasicHttp" sendTimeout="00:10:00" transferMode="Buffered" maxReceivedMessageSize="2147483647" textEncoding="utf-8" receiveTimeout="00:10:00">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None" />
    </binding>
  </basicHttpBinding>-->
  <basicHttpBinding>
    <binding name="BindingbasicHttp_Service" closeTimeout="00:10:00"
       openTimeout="00:10:00" receiveTimeout="00:10:00" sendTimeout="00:10:00"
       allowCookies="false" bypassProxyOnLocal="false" hostNameComparisonMode="StrongWildcard"
       maxBufferSize="2147483647" maxBufferPoolSize="2147483647" maxReceivedMessageSize="2147483647"
       messageEncoding="Text" textEncoding="utf-8" transferMode="Buffered"
       useDefaultWebProxy="true">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None">
        <transport clientCredentialType="None" proxyCredentialType="None"
            realm="" />
        <message clientCredentialType="UserName" algorithmSuite="Default" />
      </security>
    </binding>
    <binding name="BindingbasicHttp" closeTimeout="00:10:00"
        openTimeout="00:10:00" receiveTimeout="00:10:00" sendTimeout="00:10:00"
        allowCookies="false" bypassProxyOnLocal="false" hostNameComparisonMode="StrongWildcard"
        maxBufferSize="2147483647" maxBufferPoolSize="2147483647" maxReceivedMessageSize="2147483647"
        messageEncoding="Text" textEncoding="utf-8" transferMode="Buffered"
        useDefaultWebProxy="true">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None">
        <transport clientCredentialType="None" proxyCredentialType="None"
            realm="" />
        <message clientCredentialType="UserName" algorithmSuite="Default" />
      </security>
    </binding>
  </basicHttpBinding>
  <webHttpBinding>
    <binding name="BindingwebHttp" sendTimeout="00:10:00" transferMode="Buffered" maxReceivedMessageSize="2147483647" writeEncoding="utf-8" receiveTimeout="00:10:00">
      <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647" maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647"/>
      <security mode="None"/>
    </binding>
  </webHttpBinding>
</bindings>
