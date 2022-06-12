<%@ webservice language="C#" class="$safeitemrootname$" %>

using System;
using System.Web.Services;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web.Services;

[ WebService( Namespace="http://crystaldecisions.com/reportwebservice/9.1/" ) ]
public class $safeitemrootname$ : ReportServiceBase
{
    public $safeitemrootname$()
    {
        //
        // TODO: Add any constructor code required
        //
        this.ReportSource = this.Server.MapPath( "$fileinputname$.rpt" );
    }
}


