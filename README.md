# 携程 SOA 架构

之前在携程 工作时的一个项目架构，本架构 是基于 WCF ,MVC ，AOP,IOC,DI，T4 ，动态生成配置，伪DDD 等 分布式SOA 框架 ，
这套架构就目前来说 所应用的 技术并不是太先进，但是依然 是 一套稳定，扩展性比较好的架构 。


技 特点：WCF ,MVC ，AOP,IOC,DI ，T4 模板生成代码 ，读写分离，动态模板生成网站配置文件（对于各个测试，生产环境适用性非常好）

部署注意：
1.请把tool 文件下面的DevTools 文件夹拷贝到 C盘 
2.请把tool 文件下面的WebSites 文件夹拷贝到 d盘
3.TOOL 文件 sql.txt是 数据库生成脚本 


T4  模板
请安装T4TOOLBOX.MSI 在DLL 文件里
项目Codegen 默认情况下 是不加载的 ，只是用于生成 business,DAL,MODEL,存储过程等代码 (加载后 会报很多错误)，只需在生成代码时加载，
生成 完后 把代码文件 复制到各个项目 ，然后再卸载。

如何使用？
只需打开 codegen 里面的 T4Script.tt 然后 ctrl + s 保存即可。
