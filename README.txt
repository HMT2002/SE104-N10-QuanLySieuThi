Before use the program, you need to follow these step to create database and change the connection to the database
Step 1: open folder ScriptsForDatabase\ScriptrForDatabase.sql and run the whole script
Step 2: open App.config, change 

	<connectionStrings>
		<!--<add name="QUANLYSIEUTHIEntities" connectionString="metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\SQLEXPRESS ;initial catalog=QUANLYSIEUTHI;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />-->
		<add name="QUANLYSIEUTHIEntities" connectionString="metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\mssqlserver01 ;initial catalog=QUANLYSIEUTHI;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
	</connectionStrings>

to

	<connectionStrings>
		<add name="QUANLYSIEUTHIEntities" connectionString="metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=.\Your database name ;initial catalog=QUANLYSIEUTHI;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
	</connectionStrings>

Username: admin123
Password: admin123

*Notice*: If there is any package missing, change name folder "packages - Copy" to "packages"