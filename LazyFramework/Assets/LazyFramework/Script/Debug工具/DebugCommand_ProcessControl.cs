using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lazy
{
    public partial class DebugCommand_ProcessControl : DebugCommandBase
    {
        protected override void AddCommand()
        {
            string[] cutCommand;
            AddCommandFunc((command) =>
            {
                cutCommand = CutCommand(command);
                if (cutCommand[0] == "process" && cutCommand[2] == "step")
                {
                    if (cutCommand.Length == 4)
                    {
                        Debug.Log(command);
                        ProcessMgr.Instance.JumpStep(cutCommand[1], cutCommand[3]);
                    }
                }
            });
        }
    }

}
