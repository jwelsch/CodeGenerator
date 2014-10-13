# CodeGenerator

Dynamically generates code.

## Command Line Usage

General command line:
```
CodeGenerator.exe code_template_path replacement_path generated_code_path [-overwrite] [-silent]
```

Arguments:

* ```code_template_path```: (Mandatory) Path to the file containing the template file used to generate code.
* ```replacement_path```: (Mandatory) Path to the file containing the replacement strings that will be used in the template file.
* ```generated_code_path```: (Mandatory) Path of the file that will contain the generated code.  This may contain placeholders found in the replacement file.
* ```-overwrite```: (Optional) If this argument is present the generated output file will overwrite any file that has the same path.
* ```-silent```: (Optional) If this argument is present no output is written to the console.

## Code Template File

The code template file should contain code with placeholder text that will be replaced when CodeGenerator is run.  Demarcation can be anything that is not confused with the legitimate code.  Characters that are illegal in placeholders include:
* Space (' ')
* Newline ('\n')
* Carriage return ('\r')
* Open square bracket ('[')
* Close square bracket (']')
* Caret ('^')


## Replacement File

The replacement file should contain the following name/value pairs separated by a carriage return and newline:
```
placeholder-1=replacement text 1\r\n
placeholder-2=replacement text 2\r\n
...
placeholder-n=replacement text n
```