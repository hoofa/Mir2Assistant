﻿using Mir2Assistant.Common.Models;
using Mir2Assistant.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mir2Assistant.Common.Functions;
/// <summary>
/// 技能
/// </summary>
public static class SkillFunction
{
    /// <summary>
    /// 读取角色技能列表
    /// </summary>
    /// <param name="gameInstance"></param>
    public static void ReadSkills(MirGameInstanceModel gameInstance)
    {
        Task.Run(() =>
        {
            bool isXF = false;
            gameInstance.Skills.Clear();
            var memoryUtils = gameInstance!.MemoryUtils!;
            var addr = memoryUtils.GetMemoryAddress(gameInstance.MirConfig["技能基址"], 0x4, 0);
            while (memoryUtils.ReadToInt(addr) != 0)
            {
                var length = (byte)memoryUtils.ReadToChar(memoryUtils.GetMemoryAddress(addr, 0));
                var name = memoryUtils.ReadToBytes(memoryUtils.GetMemoryAddress(addr, 1), length);
                if (length <= 0 || length > 12)
                {
                    break;
                }
                var skill = new SkillModel
                {
                    Addr = memoryUtils.ReadToInt(addr),
                    Length = length,
                    Name = Encoding.GetEncoding("gb2312").GetString(name),
                    Type = (byte)memoryUtils.ReadToChar(memoryUtils.GetMemoryAddress(addr, 15)),
                };
                if(!isXF && skill.Type == 4)
                {
                    skill.IsXF = true;
                    isXF = true;
                }
                gameInstance.Skills.Add(skill);
                addr += 4;
            }
        });
    }




    /// <summary>
    /// 技能CAll
    /// </summary>
    public static void SkillCall(MirGameInstanceModel gameInstance, int skillAddr)
    {
        SendMirCall.Send(gameInstance, 2001, new nint[] { skillAddr, gameInstance!.MirConfig["技能CALL地址"] });
    }

}
