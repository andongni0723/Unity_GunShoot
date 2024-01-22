using System;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;



public class WebGLEmscriptenBuildFixer : IPreprocessBuildWithReport
{
    public int callbackOrder => 1;
    public void OnPreprocessBuild(BuildReport report)
    {
        Environment.SetEnvironmentVariable("PYTHONUTF8", "1");
    }

}
