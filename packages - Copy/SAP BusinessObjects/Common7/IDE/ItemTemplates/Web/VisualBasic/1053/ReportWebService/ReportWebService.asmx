<%@ webservice language="VB" class="$safeitemrootname$" %>

Imports System
Imports System.Web.Services
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Web.Services

< WebService( Namespace:="http://crystaldecisions.com/reportwebservice/9.1/" ) > _
Public Class $safeitemrootname$
    Inherits ReportServiceBase

    Public Sub New()
        Me.ReportSource = Me.Server.MapPath("$fileinputname$.rpt")
    End Sub
End Class



 