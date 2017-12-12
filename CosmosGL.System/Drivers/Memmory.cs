using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.IL2CPU.API.Attribs;
using XSharp.Assembler;

namespace CosmosGL.System.Drivers
{
    public unsafe class Memmory
    {
        [PlugMethod(Assembler = typeof(asmCopyBytes))]
        public static void memcpy(byte* dst, byte* src, int len) { }

        public class asmCopyBytes : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                new LiteralAssemblerCode("mov esi, [esp+12]");
                new LiteralAssemblerCode("mov edi, [esp+16]");
                new LiteralAssemblerCode("cld");
                new LiteralAssemblerCode("mov ecx, [esp+8]");
                new LiteralAssemblerCode("rep movsb");
            }
        }

        [PlugMethod(Assembler = typeof(asmCopyUint))]
        public static void memcpy(uint* dst, uint* src, int len) { }

        public class asmCopyUint : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                new LiteralAssemblerCode("mov esi, [esp+12]");
                new LiteralAssemblerCode("mov edi, [esp+16]");
                new LiteralAssemblerCode("cld");
                new LiteralAssemblerCode("mov ecx, [esp+8]");
                new LiteralAssemblerCode("rep movsd");
            }
        }

        [PlugMethod(Assembler = typeof(asmSetByte))]
        public static void memset(byte* dst, byte value, uint len) { }

        public class asmSetByte : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                new LiteralAssemblerCode("mov al, [esp+12]");
                new LiteralAssemblerCode("mov edi, [esp+16]");
                new LiteralAssemblerCode("cld");
                new LiteralAssemblerCode("mov ecx, [esp+8]");
                new LiteralAssemblerCode("rep stosb");
            }
        }

        [PlugMethod(Assembler = typeof(asmSetUint))]
        public static void memset(uint* dst, uint value, uint len) { }

        public class asmSetUint : AssemblerMethod
        {
            public override void AssembleNew(Assembler aAssembler, object aMethodInfo)
            {
                new LiteralAssemblerCode("mov eax, [esp+12]");
                new LiteralAssemblerCode("mov edi, [esp+16]");
                new LiteralAssemblerCode("cld");
                new LiteralAssemblerCode("mov ecx, [esp+8]");
                new LiteralAssemblerCode("rep stosd");
            }
        }
    }
}
