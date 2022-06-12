Namespace My

    Class MyApplication

#If _MyType = "WindowsForms" Then
        <Global.System.Diagnostics.DebuggerStepThrough()> _
        Protected Overrides Function OnInitialize(ByVal commandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String)) As Boolean
            Me.MinimumSplashScreenDisplayTime = 2000
            Return MyBase.OnInitialize(commandLineArgs)
        End Function
#End If

    End Class

End Namespace
