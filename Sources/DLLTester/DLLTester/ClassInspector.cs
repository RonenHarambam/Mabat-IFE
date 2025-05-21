using System;
using System.Collections.Generic;
using System.Reflection;

using System;

using System.Windows.Forms;
using DLLTester;

namespace DLLTester
{
    public class ClassInspector
    {
        public List<MethodInfo> FunctionList { get; private set; } = new List<MethodInfo>();
        private Type targetType;
        private object _classInstance;

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        public void LoadGUI(Type type)
        {
            LoadClass(type);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the application with FrmTesterGUI
            Application.Run(new s(this));
        }

        /// <summary>
        /// Load a class type and store its methods.
        /// </summary>
        /// <param name="type">The type of the class to inspect</param>
        private void LoadClass(Type type)
        {
            if (type == null)
            {
                Console.WriteLine("Invalid class type!");
                return;
            }

            targetType = type;

            // Create an instance if not static
            _classInstance = Activator.CreateInstance(targetType);

            ListFunctions();
        }

        /// <summary>
        /// List all public functions and store them
        /// </summary>
        private void ListFunctions()
        {
            FunctionList.Clear(); // Clear previous data

            foreach (MethodInfo method in targetType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                FunctionList.Add(method);
            }
        }

        /// <summary>
        /// Invoke the method with method info and string as parameters array
        /// </summary>
        /// <param name="method">The method to invoke</param>
        /// <param name="parameterValues">Array of parameter values as strings</param>
        /// <returns>The method result as a string</returns>
        internal string InvokeMethod(MethodInfo method, string[] parameterValues)
        {
            // Get the parameters of the method
            var parameters = method.GetParameters();

            // Create an array to store the converted parameter values
            object[] convertedParameters = new object[parameters.Length];

            // Convert each string parameter to the appropriate type
            for (int i = 0; i < parameters.Length; i++)
            {
                // If we have a parameter value provided and it's not beyond the input array
                if (i < parameterValues.Length && parameterValues[i] != null)
                {
                    convertedParameters[i] = ConvertStringToParameterType(parameterValues[i], parameters[i].ParameterType);
                }
                else
                {
                    // Use the default value if available, otherwise null
                    convertedParameters[i] = parameters[i].IsOptional
                        ? parameters[i].DefaultValue
                        : null;
                }
            }

            // Determine if the method is static or instance-based
            object instance = method.IsStatic ? null : _classInstance;

            // Invoke the method using the converted parameters
            try
            {
                object result = method.Invoke(instance, convertedParameters);
                return result?.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error invoking method: {ex.InnerException?.Message ?? ex.Message}");
                return ex.InnerException?.Message ?? ex.Message;
            }
        }

        /// <summary>
        /// Convert string with type to the value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private object ConvertStringToParameterType(string value, Type targetType)
        {
            if (string.IsNullOrEmpty(value))
                return null; // Handle null or empty input if desired

            if (targetType == typeof(string))
            {
                return value; // Direct assignment for strings
            }
            else if (targetType == typeof(int))
            {
                return int.TryParse(value, out int result) ? result : 0; // Convert to int, default to 0
            }
            else if (targetType == typeof(bool))
            {
                return bool.TryParse(value, out bool result) ? result : false; // Convert to bool, default to false
            }
            else if (targetType == typeof(double))
            {
                return double.TryParse(value, out double result) ? result : 0.0; // Convert to double, default to 0.0
            }
            else if (targetType == typeof(float))
            {
                return float.TryParse(value, out float result) ? result : 0f; // Convert to float, default to 0f
            }
            else if (targetType == typeof(long))
            {
                return long.TryParse(value, out long result) ? result : 0L; // Convert to long, default to 0L
            }
            else if (targetType == typeof(DateTime))
            {
                return DateTime.TryParse(value, out DateTime result) ? result : DateTime.MinValue; // Convert to DateTime
            }
            else if (targetType.IsEnum) // Correctly check for enum types
            {
                try
                {
                    // Parse the string to the enum value
                    object enumValue = Enum.Parse(targetType, value, true); // Case-insensitive
                                                                            // Convert to the underlying numeric type
                    return Convert.ChangeType(enumValue, Enum.GetUnderlyingType(targetType));
                }
                catch (ArgumentException)
                {
                    // Return default (0) for the underlying type if parsing fails
                    Type underlyingType = Enum.GetUnderlyingType(targetType);
                    if (underlyingType == typeof(int))
                        return 0;
                    else if (underlyingType == typeof(uint))
                        return 0U;
                    else if (underlyingType == typeof(byte))
                        return (byte)0;
                    else if (underlyingType == typeof(short))
                        return (short)0;
                    else if (underlyingType == typeof(ushort))
                        return (ushort)0;
                    else if (underlyingType == typeof(long))
                        return 0L;
                    else if (underlyingType == typeof(ulong))
                        return 0UL;
                    return 0; // Default to int if unknown
                }
            }

            // Add other types as needed...
            throw new InvalidCastException($"Unsupported parameter type: {targetType}");
        }
    }
}