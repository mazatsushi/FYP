*Last Updated: 28 May 2012*

#Summary

As broadband continues to permeate our lives, it is also expected to play an increasing bigger role in our healthcare system as well.

Today, it is not unusual to see large medical institutions using information systems to run their various processes automatically. However, smaller medical institutions are either still using pen-and-paper or a simple computer terminal - which is insufficient for all but the most basic medical needs.

This project aims to bridge that gap by developing (from scratch) a specific subsystem commonly used in medical institutions, the Radiology Information System, so that it may be hosted on the cloud. 

This is the final year project, or graduation thesis, of the author for his Computer Science degree.

***

#Project Information

This is the final year project, or graduation thesis, of the author to get acquainted with Java Enterprise Edition.

***

##Documentation

For detailed information regarding the system design, and its various sub-components, please refer to the report file located in the *Project Root/Documentation* folder.

***

##Getting Started
* Install all the software as specified in the section below
    * Note: When installating [Microsoft SQL Server 2008 R2], remember to enable FileStream[[1]]

* Download the repository

* Attach the instance of SQL Server located in the *Project Root/App_Data* folder

* Create a SQL Server FileStream file group in the attached instance[[2]]

* Run the script *Create DicomImages.sql* located in the *Project Root/SQL Scripts* inside the SQL Server Management Studio environment
                                                                                  
* Open the project **as a website project** in Microsoft Visual Studio

* Run the project

***

##Software Used
* [AJAX Control Toolkit]
* [ASP.NET]
* [EO.Web 2012 for ASP.NET]
* [Evil DICOM]
* [Image Resizer]
* [Microsoft SQL Server 2008 R2]
* [Microsoft Visual Studio 2010]
* [skmValidators]

[1]: http://msdn.microsoft.com/en-us/library/cc645923.aspx
[2]: http://msdn.microsoft.com/en-us/library/cc645585
[AJAX Control Toolkit]: http://ajaxcontroltoolkit.codeplex.com/
[ASP.NET]: http://www.asp.net/web-forms
[EO.Web 2012 for ASP.NET]: http://www.essentialobjects.com/
[Evil DICOM]: http://evildicom.rexcardan.com/
[Image Resizer]: http://imageresizing.net/
[Microsoft SQL Server 2008 R2]: http://www.microsoft.com/sqlserver/en/us/default.aspx
[Microsoft Visual Studio 2010]: http://www.microsoft.com/visualstudio/en-us/products/2010-editions/ultimate/overview
[skmValidators]: http://www.4guysfromrolla.com/articles/092006-1.aspx

##Support
If there are any problems, or inquiries, please do not hesitate the author to contact via email with the heading **SCE11-0353**. 