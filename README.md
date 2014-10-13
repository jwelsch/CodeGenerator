# CodeGenerator

Dynamically generates code.

## Command Line Usage

General command line:
```
CodeGenerator.exe code_template_path replacement_path generated_code_path [-overwrite] [[-silent]|[-verbose]]
```

Arguments:

* ```code_template_path```: (Mandatory) Path to the file containing the template file used to generate code.
* ```replacement_path```: (Mandatory) Path to the file containing the replacement strings that will be used in the template file.
* ```generated_code_path```: (Mandatory) Path of the file that will contain the generated code.  This may contain placeholders found in the replacement file.
* ```-overwrite```: (Optional) If this argument is present the generated output file will overwrite any file that has the same path.
* ```-silent```: (Optional) If this argument is present no output is written to the console.  Mutually exclusive with "-verbose".
* ```-verbose```: (Optional) If this argument is present detailed output is written to the output.  Mutually exclusive with "-silent".

## Code Template File

The code template file should contain code with placeholder text that will be replaced when CodeGenerator is run.  Demarcation can be anything that is not confused with the legitimate code.  Characters that are illegal in placeholders include:

* Space (' ')
* Newline ('\n')
* Carriage return ('\r')
* Open square bracket ('[')
* Close square bracket (']')
* Caret ('^')

## Replacement File

The replacement file contains placeholder and replacement text pairs.  Placeholder and replacement text should be separated by an equal sign ('=').  Pairs of placeholder and replacement text should be separated by a carriage return ('\r') and newline ('\n').  The file format supports a nested structure that can be used to special case replacement text.  Square brackets ('[' and ']') denote groups of placeholder and replacement text pairs that are used to generate code files.  Example:
```
[
   placeholder-1=replacement text 1
   placeholder-2=replacement text 2
   [
     placeholder-3=replacement text 3A
   ]
   [
     placeholder-3=replacement text 3B
   ]
]
```

In the above example, two code files will be generated.  The first will replace "placeholder-3" with "replacement text 3A".  The second will replace "placeholder-3" with "replacement text 3B".  Both will replace "placeholder-1" with "replacement text 1" and "placeholder-2" with "replacement text 2".