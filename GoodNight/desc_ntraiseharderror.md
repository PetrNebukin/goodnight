# NtRaiseHardError

This function sends a HARDERROR_MSG LPC message to the listener, typically CSRSS.EXE.
It is used to raise a hard error in the system.

The function takes the following parameters:

- ErrorStatus: The error code.
  Any hexademical code in format 0xFFFFFFFF
- NumberOfParameters: The number of optional parameters in the Parameters array.
- UnicodeStringParameterMask: An optional string parameter. Only one per error code is allowed.
- Parameters: An array of DWORD parameters for use in the error message string.
- ResponseOption: Specifies the possible values for the response to the hard error.
  See HARDERROR_RESPONSE_OPTION for more information.
  6 - reboot after error, 5 - stay on hard error
- Response: A pointer to the HARDERROR_RESPONSE enumeration.

NtRaiseHardError(ErrorStatus, NumberOfParameters, UnicodeStringParameterMask, Parameters, ValidResponseOption, Response)

```csharp
    // Here 0xc0000022 is an error code, 6 - reboot after dumping
    NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out response);
```