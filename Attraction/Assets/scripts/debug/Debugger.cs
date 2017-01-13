using UnityEngine;
using System.Collections;

namespace Debug {

	/// Delegate class for Unity's Debug class
	/// Will only permit debugging when Constants.DEBUG is true
	/// See the Constants class
	public class Debugger {

		static string _messagePrefix;

		public static void Log(string _message, DebugFlag _debugFlag)
		{
			if (DebugWithFlag(ref _messagePrefix, _debugFlag))
			{
				string _colorCode = (_debugFlag == DebugFlag.EVENT) ? "<color=#0F0>" :
									(_debugFlag == DebugFlag.TASK) ? "<color=#0FE>" :
									(_debugFlag == DebugFlag.STEP) ? "<color=#FFF>" :
									(_debugFlag == DebugFlag.LOAD) ? "<color=#F70>" : "<color=#FFF>";
				string _completeMessage = _colorCode + _messagePrefix + _message + "</color>";

				_completeMessage = MultiLineLog(_completeMessage, _colorCode);

				UnityEngine.Debug.Log(_completeMessage);
			}
		}

		public static void LogWarning(string _message)
		{
			if (DebugConst.DEBUG_WARNINGS)
			{
				_message = MultiLineLog(_message, "<color=#FC0>");
				UnityEngine.Debug.LogWarning("<color=#FC0>warning: " + _message + "</color>");
			}
		}

		public static void LogError(string _message)
		{
			if (DebugConst.DEBUG_ERRORS)
			{
				_message = MultiLineLog(_message, "<color=#F20>");
				UnityEngine.Debug.LogError("<color=#F20>error: " + _message + "</color>");
			}
		}

		static bool DebugWithFlag(ref string _prefix, DebugFlag _debugFlag)
		{
			bool _ret = false;
			switch (_debugFlag)
			{
				case DebugFlag.EVENT: 
					_prefix = "event: ";
					if (DebugConst.DEBUG_EVENTS)
						_ret =  true;
					break;
				case DebugFlag.TASK: 
					_prefix = "task: ";
					if (DebugConst.DEBUG_TASKS)
						_ret =  true;
					break;
				case DebugFlag.STEP:
					_prefix = "step: "; 
					if (DebugConst.DEBUG_STEPS)
						_ret =  true;
					break;
				case DebugFlag.LOAD:
					_prefix = "load: "; 
					if (DebugConst.DEBUG_LOAD)
						_ret = true;
					break;
			}

			return _ret;
		}

		static string MultiLineLog(string _logMessage, string _colorCode)
		{
			if (_logMessage.Contains("\n"))
			{
				//add closing color tags at each newline in _logMessage
				char _newLine = '\n';
				char _curr = 'a';
				string _tags = "";
				string _new = "";
				for (int i = 0; i < _logMessage.Length - 1; i++)
				{
					_curr = _logMessage[i];

					if (_curr == _newLine)
					{
						_tags = "</color>\n" + _colorCode;
						_new = _logMessage.Substring(0, i) + _tags + _logMessage.Substring(i+1);
						_logMessage = _new;
						i += _tags.Length;
					}
				}
			}

			return _logMessage;
		}

	}
}
