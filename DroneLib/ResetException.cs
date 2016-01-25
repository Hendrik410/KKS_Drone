using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public enum ResetException
    {
        None = -1,
        IllegalInstruction = 0,
        Syscall = 1,
        InstructionFetchError = 2,
        LoadStoreError = 3,
        Level1Interrupt = 4,
        Alloca = 5,
        IntegerDivideByZero = 6,
        ReservedForTensilica7 = 7,
        Privileged = 8,
        LoadStoreAlignment = 9,
        ReservedForTensilica10 = 10,
        ReservedForTensilica11 = 11,
        InstrPIFDataError = 12,
        LoadStorePIFDataError = 13,
        InstrPIFAddrError = 14,
        LoadStorePIFAddrError = 15,
        InstTLBMiss = 16,
        InstTLBMultiHit = 17,
        InstFetchPrivilege = 18,
        ReservedForTensilica19 = 19,
        InstFetchProhibited = 20,
        ReservedForTensilica21 = 21,
        ReservedForTensilica22 = 22,
        ReservedForTensilica23 = 23,
        LoadStoreTLBMiss = 24,
        LoadStoreTLBMultiHit = 25,
        LoadStorePrivilege = 26,
        ReservedForTensilica27 = 27,
        LoadProhibited = 28,
        StoreProhibited = 29,

    }
}
