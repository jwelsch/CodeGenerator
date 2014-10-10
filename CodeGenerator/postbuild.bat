@ECHO OFF
SET ConfigurationName=%~1
SET SolutionDir=%~2
SET ProjectDir=%~3
SET TargetDir=%~4
SET TargetPath=%~5
xcopy /y "%ProjectDir%Test Files\CSharpTemplate.template" "%TargetDir%"
xcopy /y "%ProjectDir%Test Files\CSharpReplacement.replace" "%TargetDir%"
IF /I "%ConfigurationName%" == "Release" (
  @ECHO ON
  "%SolutionDir%packages\ilmerge.2.13.0307\ILMerge.exe" /out:"%TargetDir%CodeGeneratorM.exe" "%TargetPath%" "%TargetDir%CommandLineLib.dll"
  del "%TargetPath%"
  move "%TargetDir%CodeGeneratorM.exe" "%TargetPath%"
)