Option Strict Off
Option Explicit On

Imports System
Imports System.IO
Imports System.Resources

Namespace My.Resources
    
    <Global.Microsoft.VisualBasic.HideModuleName()>  _
    Module MyResources
        
        Private _resMgr As System.Resources.ResourceManager
        
        Private _resCulture As System.Globalization.CultureInfo
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public ReadOnly Property ResourceManager() As System.Resources.ResourceManager
            Get
                If (_resMgr Is Nothing) Then
                    Dim temp As System.Resources.ResourceManager = New System.Resources.ResourceManager("$safeprojectname$.MyResources", GetType(MyResources).Assembly)
                    System.Threading.Thread.MemoryBarrier
                    _resMgr = temp
                End If
                Return _resMgr
            End Get
        End Property
        
        <System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Public Property Culture() As System.Globalization.CultureInfo
            Get
                Return _resCulture
            End Get
            Set
                _resCulture = value
            End Set
        End Property
    End Module
End Namespace
