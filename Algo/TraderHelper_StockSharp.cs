namespace StockSharp.Algo
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using Ecng.Common;
	using Ecng.Collections;
	using Ecng.IO;
	using Ecng.Net;
	using Ecng.Serialization;

	using MoreLinq;

	using StockSharp.Algo;
	using StockSharp.Configuration;
	using StockSharp.Messages;

	partial class TraderHelper
	{
		#region Ignore codes

		private static readonly ISet<string> _ignoreCodes = @"SPFB.ALSI
SPFB.AUDU
SPFB.BR
SPFB.BR-11.08
SPFB.CHMF
SPFB.CHMF-9.08
SPFB.CRN
SPFB.CRNU
SPFB.CTN
SPFB.CTNU
SPFB.CU
SPFB.DIZL-10.07
SPFB.DIZL-10.08
SPFB.DIZL-11.07
SPFB.DIZL-11.08
SPFB.DIZL-12.07
SPFB.DIZL-2.08
SPFB.DIZL-3.08
SPFB.DIZL-4.07
SPFB.DIZL-4.08
SPFB.DIZL-5.07
SPFB.DIZL-5.08
SPFB.DIZL-6.07
SPFB.DIZL-6.08
SPFB.DIZL-7.07
SPFB.DIZL-7.08
SPFB.DIZL-8.07
SPFB.DIZL-8.08
SPFB.DIZL-9.07
SPFB.DIZL-9.08
SPFB.DS
SPFB.EB30-12.06
SPFB.EB30-3.07
SPFB.EB30-6.06
SPFB.EB30-6.07
SPFB.EB30-9.06
SPFB.EB30-9.07
SPFB.ECBM
SPFB.ECBQ
SPFB.ECBY
SPFB.ECPM
SPFB.ED
SPFB.EERU
SPFB.EERU-12.02
SPFB.EERU-12.03
SPFB.EERU-12.04
SPFB.EERU-12.05
SPFB.EERU-12.06
SPFB.EERU-12.07
SPFB.EERU-3.03
SPFB.EERU-3.04
SPFB.EERU-3.05
SPFB.EERU-3.06
SPFB.EERU-3.07
SPFB.EERU-3.08
SPFB.EERU-5.08
SPFB.EERU-6.03
SPFB.EERU-6.04
SPFB.EERU-6.05
SPFB.EERU-6.06
SPFB.EERU-6.07
SPFB.EERU-6.08
SPFB.EERU-9.02
SPFB.EERU-9.03
SPFB.EERU-9.04
SPFB.EERU-9.05
SPFB.EERU-9.06
SPFB.EERU-9.07
SPFB.EERx-12.07
SPFB.EUBM
SPFB.EUBQ
SPFB.EUBY
SPFB.EUPM
SPFB.Eu
SPFB.FEES
SPFB.FS04-12.07
SPFB.FS04-3.08
SPFB.FS04-6.07
SPFB.FS04-6.08
SPFB.FS04-9.07
SPFB.FS04-9.08
SPFB.G018-12.07
SPFB.G018-3.08
SPFB.G018-6.08
SPFB.G018-9.07
SPFB.G018-9.08
SPFB.G020-12.07
SPFB.G020-3.08
SPFB.G020-6.08
SPFB.G020-9.07
SPFB.G020-9.08
SPFB.G021-12.07
SPFB.G021-3.08
SPFB.G021-6.08
SPFB.G021-9.07
SPFB.G021-9.08
SPFB.G061-12.07
SPFB.G061-3.08
SPFB.G061-6.08
SPFB.G061-9.07
SPFB.G061-9.08
SPFB.G199-12.07
SPFB.G199-3.08
SPFB.G199-6.08
SPFB.G199-9.07
SPFB.G199-9.08
SPFB.GAZR
SPFB.GAZR-12.02
SPFB.GAZR-12.03
SPFB.GAZR-12.04
SPFB.GAZR-12.05
SPFB.GAZR-12.06
SPFB.GAZR-12.07
SPFB.GAZR-3.03
SPFB.GAZR-3.04
SPFB.GAZR-3.05
SPFB.GAZR-3.06
SPFB.GAZR-3.07
SPFB.GAZR-3.08
SPFB.GAZR-6.03
SPFB.GAZR-6.04
SPFB.GAZR-6.05
SPFB.GAZR-6.06
SPFB.GAZR-6.07
SPFB.GAZR-6.08
SPFB.GAZR-9.02
SPFB.GAZR-9.03
SPFB.GAZR-9.04
SPFB.GAZR-9.05
SPFB.GAZR-9.06
SPFB.GAZR-9.07
SPFB.GAZR-9.08
SPFB.GB02-6.04
SPFB.GBMW
SPFB.GBPU
SPFB.GDBK
SPFB.GMKR
SPFB.GMKR-12.04
SPFB.GMKR-12.05
SPFB.GMKR-12.06
SPFB.GMKR-12.07
SPFB.GMKR-3.05
SPFB.GMKR-3.06
SPFB.GMKR-3.07
SPFB.GMKR-3.08
SPFB.GMKR-6.05
SPFB.GMKR-6.06
SPFB.GMKR-6.07
SPFB.GMKR-6.08
SPFB.GMKR-9.05
SPFB.GMKR-9.06
SPFB.GMKR-9.07
SPFB.GMKR-9.08
SPFB.GOLD
SPFB.GOLD-12.06
SPFB.GOLD-12.07
SPFB.GOLD-3.07
SPFB.GOLD-3.08
SPFB.GOLD-5.07
SPFB.GOLD-6.07
SPFB.GOLD-6.08
SPFB.GOLD-7.06
SPFB.GOLD-8.06
SPFB.GOLD-9.06
SPFB.GOLD-9.07
SPFB.GOLD-9.08
SPFB.GR
SPFB.GRU
SPFB.GSIE
SPFB.GSL
SPFB.GSLU
SPFB.GZ08-12.07
SPFB.GZ08-3.08
SPFB.GZ08-6.07
SPFB.GZ08-6.08
SPFB.GZ08-9.07
SPFB.GZ08-9.08
SPFB.GZ09-12.07
SPFB.GZ09-3.08
SPFB.GZ09-6.08
SPFB.GZ09-9.08
SPFB.HSIF
SPFB.HYDR
SPFB.HYDR-9.08
SPFB.IBVS
SPFB.LKOH
SPFB.LKOH-12.02
SPFB.LKOH-12.03
SPFB.LKOH-12.04
SPFB.LKOH-12.05
SPFB.LKOH-12.06
SPFB.LKOH-12.07
SPFB.LKOH-3.03
SPFB.LKOH-3.04
SPFB.LKOH-3.05
SPFB.LKOH-3.06
SPFB.LKOH-3.07
SPFB.LKOH-3.08
SPFB.LKOH-6.03
SPFB.LKOH-6.04
SPFB.LKOH-6.05
SPFB.LKOH-6.06
SPFB.LKOH-6.07
SPFB.LKOH-6.08
SPFB.LKOH-9.02
SPFB.LKOH-9.03
SPFB.LKOH-9.04
SPFB.LKOH-9.05
SPFB.LKOH-9.06
SPFB.LKOH-9.07
SPFB.LKOH-9.08
SPFB.MB10-10.06
SPFB.MB10-11.06
SPFB.MB10-12.06
SPFB.MB10-12.07
SPFB.MB10-2.07
SPFB.MB10-3.07
SPFB.MB10-3.08
SPFB.MB10-6.06
SPFB.MB10-6.07
SPFB.MB10-6.08
SPFB.MB10-9.06
SPFB.MB10-9.07
SPFB.MB10-9.08
SPFB.MB27-12.03
SPFB.MB29-3.04
SPFB.MB29-6.04
SPFB.MB3-12.05
SPFB.MB3-3.06
SPFB.MB3-6.06
SPFB.MB3-9.05
SPFB.MB3-9.06
SPFB.MB43-9.05
SPFB.MB47-1.06
SPFB.MEXC
SPFB.MGNT
SPFB.MIBR
SPFB.MIBR-1.07
SPFB.MIBR-10.06
SPFB.MIBR-10.08
SPFB.MIBR-11.06
SPFB.MIBR-11.08
SPFB.MIBR-12.06
SPFB.MIBR-12.07
SPFB.MIBR-2.08
SPFB.MIBR-3.07
SPFB.MIBR-3.08
SPFB.MIBR-4.08
SPFB.MIBR-5.08
SPFB.MIBR-6.06
SPFB.MIBR-6.07
SPFB.MIBR-6.08
SPFB.MIBR-7.08
SPFB.MIBR-8.06
SPFB.MIBR-8.08
SPFB.MIBR-9.06
SPFB.MIBR-9.07
SPFB.MIBR-9.08
SPFB.MIX
SPFB.MO06-12.07
SPFB.MO06-3.08
SPFB.MO06-6.07
SPFB.MO06-6.08
SPFB.MO06-9.07
SPFB.MO06-9.08
SPFB.MO07-12.07
SPFB.MO07-3.08
SPFB.MO07-6.08
SPFB.MO07-9.08
SPFB.MOEX
SPFB.MOPR
SPFB.MOPR-12.07
SPFB.MOPR-3.08
SPFB.MOPR-6.08
SPFB.MOPR-9.07
SPFB.MOPR-9.08
SPFB.MPRI
SPFB.MTSI
SPFB.MTSI-12.07
SPFB.MTSI-3.08
SPFB.MTSI-6.08
SPFB.MTSI-9.07
SPFB.MTSI-9.08
SPFB.NOTK
SPFB.NOTK-12.07
SPFB.NOTK-3.08
SPFB.NOTK-6.08
SPFB.NOTK-9.07
SPFB.NOTK-9.08
SPFB.OFZ2
SPFB.OFZ4
SPFB.OFZ6
SPFB.OGE-12.07
SPFB.OGKC-12.07
SPFB.OGKC-9.07
SPFB.OGKD-12.07
SPFB.OGKD-9.07
SPFB.PLD
SPFB.PLT
SPFB.PLZL
SPFB.PLZL-12.07
SPFB.PLZL-3.08
PZH1
SPFB.PLZL-6.08
SPFB.PLZL-9.07
SPFB.PLZL-9.08
SPFB.ROSN
SPFB.ROSN-12.06
SPFB.ROSN-12.07
SPFB.ROSN-3.07
SPFB.ROSN-3.08
SPFB.ROSN-6.07
SPFB.ROSN-6.08
SPFB.ROSN-9.07
SPFB.ROSN-9.08
SPFB.RTKM
SPFB.RTKM-12.02
SPFB.RTKM-12.03
SPFB.RTKM-12.04
SPFB.RTKM-12.05
SPFB.RTKM-12.06
SPFB.RTKM-12.07
SPFB.RTKM-3.03
SPFB.RTKM-3.04
SPFB.RTKM-3.05
SPFB.RTKM-3.06
SPFB.RTKM-3.07
SPFB.RTKM-3.08
SPFB.RTKM-6.03
SPFB.RTKM-6.04
SPFB.RTKM-6.05
SPFB.RTKM-6.06
SPFB.RTKM-6.07
SPFB.RTKM-6.08
SPFB.RTKM-9.02
SPFB.RTKM-9.03
SPFB.RTKM-9.04
SPFB.RTKM-9.05
SPFB.RTKM-9.06
SPFB.RTKM-9.07
SPFB.RTKM-9.08
SPFB.RTS
SPFB.RTS-12.05
SPFB.RTS-12.06
SPFB.RTS-12.07
SPFB.RTS-3.06
SPFB.RTS-3.07
SPFB.RTS-3.08
SPFB.RTS-6.06
SPFB.RTS-6.07
SPFB.RTS-6.08
SPFB.RTS-9.05
SPFB.RTS-9.06
SPFB.RTS-9.07
SPFB.RTS-9.08
SPFB.RTSS
SPFB.RTSVX
SPFB.RTSc
SPFB.RTSc-12.07
SPFB.RTSc-3.08
SPFB.RTSc-6.07
SPFB.RTSc-6.08
SPFB.RTSc-9.07
SPFB.RTSc-9.08
SPFB.RTSo
SPFB.RTSo-12.07
SPFB.RTSo-3.08
SPFB.RTSo-6.07
SPFB.RTSo-6.08
SPFB.RTSo-9.07
SPFB.RTSo-9.08
SPFB.RTSt-6.08
SPFB.RTSt-9.08
SPFB.RUAL
SPFB.RUIX-12.02
SPFB.RUIX-12.03
SPFB.RUIX-12.04
SPFB.RUIX-3.03
SPFB.RUIX-3.04
SPFB.RUIX-3.05
SPFB.RUIX-6.03
SPFB.RUIX-6.04
SPFB.RUIX-6.05
SPFB.RUIX-9.02
SPFB.RUIX-9.03
SPFB.RUIX-9.04
SPFB.RUIX-9.05
SPFB.RVI
SPFB.RZ06-12.07
SPFB.RZ06-3.08
SPFB.RZ06-6.07
SPFB.RZ06-6.08
SPFB.RZ06-9.07
SPFB.RZ06-9.08
SPFB.RZ07-12.07
SPFB.RZ07-3.08
SPFB.RZ07-6.07
SPFB.RZ07-6.08
SPFB.RZ07-9.07
SPFB.RZ07-9.08
SPFB.SBER-12.05
SPFB.SBER-12.06
SPFB.SBER-12.07
SPFB.SBER-3.06
SPFB.SBER-3.07
SPFB.SBER-3.08
SPFB.SBER-6.06
SPFB.SBER-6.07
SPFB.SBER-9.06
SPFB.SBER-9.07
SPFB.SBER-9.08
SPFB.SBN
SPFB.SBNU
SPFB.SBPR
SPFB.SBPR-3.08
SPFB.SBPR-6.08
SPFB.SBPR-9.08
SPFB.SBRF
SPFB.SBRF-3.08
SPFB.SBRF-6.08
SPFB.SBRF-9.08
SPFB.SEBM
SPFB.SILV
SPFB.SILV-12.07
SPFB.SILV-3.08
SPFB.SILV-6.08
SPFB.SILV-9.07
SPFB.SILV-9.08
SPFB.SNGP
SPFB.SNGR
SPFB.SNGR-12.02
SPFB.SNGR-12.03
SPFB.SNGR-12.04
SPFB.SNGR-12.05
SPFB.SNGR-12.06
SPFB.SNGR-12.07
SPFB.SNGR-3.03
SPFB.SNGR-3.04
SPFB.SNGR-3.05
SPFB.SNGR-3.06
SPFB.SNGR-3.07
SPFB.SNGR-3.08
SPFB.SNGR-6.03
SPFB.SNGR-6.04
SPFB.SNGR-6.05
SPFB.SNGR-6.06
SPFB.SNGR-6.07
SPFB.SNGR-6.08
SPFB.SNGR-9.02
SPFB.SNGR-9.03
SPFB.SNGR-9.04
SPFB.SNGR-9.05
SPFB.SNGR-9.06
SPFB.SNGR-9.07
SPFB.SNGR-9.08
SPFB.SNSX
SPFB.CY
SPFB.GDAI
SPFB.GVW3
SPFB.MXI
SPFB.OF10
SPFB.OF15
SPFB.RF30
SPFB.RG1EU
SPFB.RREXU
SPFB.RT1EU
SPFB.W3EXU
SPFB.W4EXU
SPFB.W5EXU
SPFB.SUGA-10.08
SPFB.SUGA-11.07
SPFB.SUGA-11.08
SPFB.SUGA-3.08
SPFB.SUGA-5.08
SPFB.SUGA-7.08
SPFB.SUGR
SPFB.SWBM
SPFB.Si
SPFB.Si-1.08
SPFB.Si-12.03
SPFB.Si-12.04
SPFB.Si-12.05
SPFB.Si-12.06
SPFB.Si-12.07
SPFB.Si-2.08
SPFB.Si-3.03
SPFB.Si-3.04
SPFB.Si-3.05
SPFB.Si-3.06
SPFB.Si-3.07
SPFB.Si-3.08
SPFB.Si-4.08
SPFB.Si-5.08
SPFB.Si-6.03
SPFB.Si-6.04
SPFB.Si-6.05
SPFB.Si-6.06
SPFB.Si-6.07
SPFB.Si-6.08
SPFB.Si-7.08
SPFB.Si-8.08
SPFB.Si-9.03
SPFB.Si-9.04
SPFB.Si-9.05
SPFB.Si-9.06
SPFB.Si-9.07
SPFB.Si-9.08
SPFB.TATN
SPFB.TATN-9.08
SPFB.TRNF
SPFB.TRNF-12.07
SPFB.TRNF-3.08
SPFB.TRNF-6.08
SPFB.TRNF-9.07
SPFB.TRNF-9.08
SPFB.UCAD
SPFB.UCHF
SPFB.UJPY
SPFB.UR
SPFB.UR-1.08
SPFB.UR-10.06
SPFB.UR-10.07
SPFB.UR-10.08
SPFB.UR-11.06
SPFB.UR-11.07
SPFB.UR-11.08
SPFB.UR-12.06
SPFB.UR-12.07
SPFB.UR-2.07
SPFB.UR-2.08
SPFB.UR-3.07
SPFB.UR-3.08
SPFB.UR-4.07
SPFB.UR-4.08
SPFB.UR-5.07
SPFB.UR-5.08
SPFB.UR-6.07
SPFB.UR-6.08
SPFB.UR-7.06
SPFB.UR-7.07
SPFB.UR-7.08
SPFB.UR-8.06
SPFB.UR-8.07
SPFB.UR-8.08
SPFB.UR-9.06
SPFB.UR-9.07
SPFB.UR-9.08
SPFB.URKA
SPFB.URSI
SPFB.URSI-12.07
SPFB.URSI-3.08
UIH1
SPFB.URSI-6.08
SPFB.URSI-9.07
SPFB.URSI-9.08
SPFB.UTRY
SPFB.UUAH
SPFB.VTBR
SPFB.VTBR-12.07
SPFB.VTBR-3.08
SPFB.VTBR-6.08
SPFB.VTBR-9.07
SPFB.VTBR-9.08
SPFB.YNDX
@18
@20
@21
@61
@99
@AU
@BR
@CA
@CB
@CF
@CH
@CN
@CP
@CT
@CU
@DS
@DZ
@EB
@ED
@EN
@ES
@Eu
@Ex
@F4
@FS
@G8
@G9
@GB
@GD
@GM
@GR
@GS
@GU
@GZ
@HY
@JP
@LK
@M0
@M3
@M6
@M7
@MB
@MI
@MP
@MR
@MT
@MX
@NK
@O2
@O4
@O6
@OC
@OD
@OE
@OV
@OX
@PD
@PT
@PZ
@QB
@R6
@R7
@Rc
@RI
@Rk
@RN
@Ro
@RO
@RS
@RT
@RX
@SA
@SB
@SG
@Si
@SN
@SO
@SP
@SR
@SU
@SV
@TN
@TT
@UB
@UI
@UK
@UP
@UR
@VB
@VX
@WB
ESM8
PZH1
SBH8
UIH1
RI00000X8
RI00000L8
RI10000L8
RI10000X8
RI00000C8
RI00000O8
RI05000C8
RI05000O8
RI10000C8
RI10000O8
RI15000C8
RI15000O8
RI20000C8
RI20000O8
RI25000C8
RI25000O8
RI30000C8
RI30000O8
RI35000C8
RI35000O8
RI40000C8
RI40000O8
RI45000C8
RI45000O8
RI50000C8
RI50000O8
RI55000C8
RI55000O8
RI60000C8
RI60000O8
RI65000C8
RI65000O8
RI70000C8
RI70000O8
RI75000C8
RI75000O8
RI80000C8
RI80000O8
RI85000C8
RI85000O8
RI90000C8
RI90000O8
RI95000C8
RI95000O8
RI00000B8
RI00000N8
RI05000B8
RI05000N8
RI10000B8
RI10000N8
RI15000B8
RI15000N8
RI20000B8
RI20000N8
RI25000B8
RI25000N8
RI30000B8
RI30000N8
RI35000B8
RI35000N8
RI40000B8
RI40000N8
RI45000B8
RI45000N8
RI50000B8
RI50000N8
RI55000B8
RI55000N8
RI60000B8
RI60000N8
RI65000B8
RI65000N8
RI70000B8
RI70000N8
RI75000B8
RI75000N8
RI80000B8
RI80000N8
RI85000B8
RI85000N8
RI90000B8
RI90000N8
RI95000B8
RI95000N8
RI00000A8
RI00000M8
RI05000A8
RI05000M8
RI10000A8
RI10000M8
RI15000A8
RI15000M8
RI20000A8
RI20000M8
RI25000A8
RI25000M8
RI30000A8
RI30000M8
RI35000A8
RI35000M8
RI40000A8
RI40000M8
RI45000A8
RI45000M8
RI50000A8
RI50000M8
RI55000A8
RI55000M8
RI60000A8
RI60000M8
RI65000A8
RI65000M8
RI70000A8
RI70000M8
RI75000A8
RI75000M8
RI80000A8
RI80000M8
RI85000A8
RI85000M8
RI90000A8
RI90000M8
RI95000A8
RI95000M8
RI00000L7
RI00000X7
RI05000L7
RI05000X7
RI10000L7
RI10000X7
RI15000L7
RI15000X7
RI20000L7
RI20000X7
RI25000L7
RI25000X7
RI30000L7
RI30000X7
RI35000L7
RI35000X7
RI40000L7
RI40000X7
RI45000L7
RI45000X7
RI50000L7
RI50000X7
RI55000L7
RI55000X7
RI60000L7
RI60000X7
RI65000L7
RI65000X7
RI70000L7
RI70000X7
RI75000L7
RI75000X7
RI80000L7
RI80000X7
RI85000L7
RI85000X7
RI90000L7
RI90000X7
RI95000L7
RI95000X7
RN7500AF9
RN7500AR9
RN8000AF9
RN8000AR9
SB00000I8
SB00000U8
SB05000I8
SB05000U8
Si42500AI9
Si42500AU9
Si42750AI9
Si42750AU9
Si43000AI9
Si43000AU9
@AUDU
@CHMF
@FEES
@GAZR
@GBPU
@GMKR
@GOLD
@HYDR
@LKOH
@MGNT
@MIX
@MOPR
@MTSI
@MXI
@NOTK
@OF10
@OF15
@OFZ2
@OFZ4
@OFZ6
@PLD
@PLT
@ROSN
@RTKM
@RTS
@RTSS
@RVI
@SBPR
@SBRF
@SILV
@SNGP
@SNGR
@SUGR
@TATN
@TRNF
@UCAD
@UCHF
@UJPY
@URKA
@UUAH
@VTBR
SPFB.RUON
SBRF
SNGR
TRNF
RI.RUTNTRR
RI.RUTNTRN
RI.RUTNTR
RI.RUTLTRR
RI.RUTLTRN
RI.RUTLTR
RI.RUSMTRR
RI.RUSMTRN
RI.RUSMTR
RI.RUOGTRR
RI.RUOGTRN
RI.RUOGTR
RI.RUMMTRR
RI.RUMMTRN
RI.RUMMTR
RI.RUFNTRR
RI.RUFNTRN
RI.RUFNTR
RI.RUEUTRR
RI.RUEUTRN
RI.RUEUTR
RI.RUCNTRR
RI.RUCNTRN
RI.RUCNTR
RI.RUCHTRR
RI.RUCHTRN
RI.RUCHTR
RI.METNTRR
RI.METNTRN
RI.METNTR
RI.METLTRR
RI.METLTRN
RI.METLTR
RI.MESMTRR
RI.MESMTRN
RI.MESMTR
RI.MEOGTRR
RI.MEOGTRN
RI.MEOGTR
RI.MEMMTRR
RI.MEMMTRN
RI.MEMMTR
RI.MEFNTRR
RI.MEFNTRN
RI.MEFNTR
RI.MEEUTRR
RI.MEEUTRN
RI.MEEUTR
RI.MECNTRR
RI.MECNTRN
RI.MECNTR
RI.MECHTRR
RI.MECHTRN
RI.MECHTR
SPFB.ALMN
SPFB.Zn
SPFB.Nl
SPFB.Co
SPFB.Al
SPFB.UINR
SPFB.GLD
SPFB.U500
SPFB.CL
BR
Eu
MIBR
EERU
RI.RUSFARON
RI.RUSFAR3M
RI.RUSFAR2W
RI.RUSFAR2M
RI.RUSFAR1W
RI.RUSFAR1M
RI.RUSFAR
RI.AKSPA
RI.GPBCBI4Y
RI.GPBCBI2Y
RI.MRSV
RI.MRRT
SPFB.MAGN
RI.RTSSTD
RI.MICEXTRN
RI.MICEXTLC
RI.MICEXPWR
RI.MICEXO&amp;G
RI.MICEXM&amp;M
RI.MICEXFNL
RI.MICEXCHM
RI.MICEXCGS
RI.MICEXMBITR
RI.MICEXMBICP
RI.MICEXINNOV
RI.MICEXCBITR5Y
RI.MICEXCBITR
RI.MICEXCBICP5Y
RI.MICEXCBICP
RI.MICEXBMI
RI.MICEX10INDEX
RUON
MXI
CY
UCAD
UTRY
RF30
MEXC
EUBY
ECBY
EUBQ
ECBQ
GVW3
GSIE
GDBK
GDAI
GBMW
UUAH
UCHF
UJPY
OF15
SBNU
GSLU
GRU
CTNU
CRNU
W5EXU
W4EXU
W3EXU
RT1EU
RREXU
RG1EU
OF10
SNSX
IBVS
HSIF
ALSI
OFZ6
MIX
OFZ4
OFZ2
CU
CRN
SBN
GR
CTN
UR
GSL
SWBM
SEBM
EUPM
EUBM
ECPM
ECBM
SUGR
MOPR
MPRI
SBPR
SNGP
DS
RTSo
RTSc
PLT
PLD
GBPU
AUDU
RTSS
ED
Si
GOLD
RTS
GMKR
GAZR
SPFB.1MFR".SplitLines().ToIgnoreCaseSet();

		#endregion

		/// <summary>
		///
		/// </summary>
		/// <param name="securities"></param>
		/// <param name="code"></param>
		/// <param name="type"></param>
		/// <param name="board"></param>
		/// <param name="secMsg"></param>
		/// <param name="onlyLocal"></param>
		public static void TryFindLocal(this IDictionary<Tuple<string, SecurityTypes>, SecurityMessage> securities, string code, SecurityTypes type, string board, SecurityMessage secMsg, bool onlyLocal = false)
		{
			if (_ignoreCodes.Contains(code))
				return;

			var secProvider = ServicesRegistry.TrySecurityProvider;

			if (secProvider != null)
			{
				var found = (board.IsEmpty() ? null : new[] { secProvider.LookupById(new SecurityId { SecurityCode = code, BoardCode = board }) }.Where(f1 => f1 != null)) ??
							secProvider.LookupByCode(code, type).Where(s => s.ToSecurityId().SecurityCode.EqualsIgnoreCase(code)) ??
				            secProvider.Lookup(new SecurityLookupMessage { ShortName = code, SecurityType = type }) ??
				            secProvider.Lookup(new SecurityLookupMessage { Name = code, SecurityType = type });

				if (type == SecurityTypes.Stock)
				{
					found = found.OrderBy(f2 =>
					{
						var id = f2.Id.ToSecurityId();

						if (id.BoardCode.EndsWithIgnoreCase("EMU"))
							return 100;

						if (id.BoardCode.StartsWithIgnoreCase("TQ"))
							return 0;

						return 50;
					});
				}

				var f = found.FirstOrDefault();
				if (f != null)
				{
					f.ToMessage().CopyTo(secMsg);
					return;
				}
			}

			if (!onlyLocal)
				securities.TryAdd2(Tuple.Create(code.ToUpperInvariant(), type), secMsg);
		}

		private class InstrumentProviderClient
		{
			public ICollection<SecurityMessage> LookupSecurities(SecurityLookupMessage criteria)
			{
				if (criteria == null)
					throw new ArgumentNullException(nameof(criteria));

				var existingSecurities = ServicesRegistry.TrySecurityProvider?.Lookup(criteria).Select(s => s.ToMessage()).Where(s => !s.SecurityId.IsAllSecurity()).ToArray() ?? Enumerable.Empty<SecurityMessage>();
				var existingIds = existingSecurities.Select(s => s.SecurityId.ToStringId()).ToIgnoreCaseSet();

				var securities = new List<SecurityMessage>();

				static byte[] Send<T>(string method, string argName, T arg)
				{
					var request = new MemoryStream();
					request.SerializeDataContract(arg);

					using (var client = new WebClientEx { Timeout = TimeSpan.FromMinutes(1) })
					{
						var response = client.UploadData($"{Paths.GetWebSiteUrl()}/services/instrumentprovider.ashx?method={method}", request.To<byte[]>().DeflateTo());
						return response.DeflateFrom();
					}
				}

				var ids = Send("LookupSecurityIds", "criteria", criteria).UTF8().SplitByDotComma();

				var newSecurityIds = ids.Where(id => !existingIds.Contains(id)).ToArray();

				foreach (var b in newSecurityIds.Batch(1000))
				{
					var batch = b.ToArray();

					var response = Send("GetSecurities", "securityIds", batch);
					securities.AddRange(response.To<Stream>().DeserializeDataContract<SecurityMessage[]>());
				}

				securities.AddRange(existingSecurities);
				return securities;
			}
		}

		private static T DeserializeDataContract<T>(this Stream stream)
		{
			return (T)new System.Runtime.Serialization.DataContractSerializer(typeof(T)).ReadObject(stream);
		}

		private static void SerializeDataContract<T>(this Stream stream, T value)
		{
			new System.Runtime.Serialization.DataContractSerializer(typeof(T)).WriteObject(stream, value);
		}

		/// <summary>
		/// Download securities info.
		/// </summary>
		/// <param name="securities">Securities.</param>
		public static void DownloadInfo(this IDictionary<Tuple<string, SecurityTypes>, SecurityMessage> securities)
		{
			if (securities.Count == 0)
				return;

			void MostAppropriated(IEnumerable<SecurityMessage> messages, Tuple<string, SecurityTypes> key, SecurityMessage destination)
			{
				SecurityMessage found = null;

				if (messages != null)
				{
					messages = messages.Where(s =>
					{
						var name = key.Item1;

						return s.SecurityId.SecurityCode.EqualsIgnoreCase(name) || s.ShortName.EqualsIgnoreCase(name) || s.Name.EqualsIgnoreCase(name);
					});

					switch (key.Item2)
					{
						case SecurityTypes.Future:
							found = messages.OrderByDescending(m => m.ExpiryDate).FirstOrDefault();
							break;
						case SecurityTypes.Stock:
							found = messages.OrderBy(m => m.SecurityId.BoardCode.StartsWithIgnoreCase("TQ") ? 0 : 1).FirstOrDefault();
							break;
					}
				}

				if (found == null)
				{
					System.Diagnostics.Debug.WriteLine("{0}={1}", key.Item1, key.Item2);
				}
				else
				{
					var native = destination.SecurityId.Native;
					found.CopyTo(destination, false);
					destination.SetNativeId(native);
				}
			}

			var client = new InstrumentProviderClient();

			if (securities.Count <= 10)
			{
				foreach (var pair in securities)
				{
					var name = pair.Key.Item1;
					var type = pair.Key.Item2;

					var found = client.LookupSecurities(new SecurityLookupMessage
					{
						SecurityId = new SecurityId { SecurityCode = name },
						SecurityType = type,
					});

					if (found.Count == 0)
					{
						found = client.LookupSecurities(new SecurityLookupMessage
						{
							SecurityType = type,
							ShortName = name
						});

						if (found.Count == 0)
						{
							found = client.LookupSecurities(new SecurityLookupMessage
							{
								SecurityType = type,
								Name = name
							});
						}
					}

					MostAppropriated(found, pair.Key, pair.Value);
				}
			}
			else
			{
				var byCode = new Dictionary<Tuple<string, SecurityTypes?>, List<SecurityMessage>>();
				var byShortName = new Dictionary<Tuple<string, SecurityTypes?>, List<SecurityMessage>>();
				var byName = new Dictionary<Tuple<string, SecurityTypes?>, List<SecurityMessage>>();

				foreach (var message in client.LookupSecurities(Messages.Extensions.LookupAllCriteriaMessage))
				{
					var secType = message.SecurityType;

					byCode.SafeAdd(Tuple.Create(message.SecurityId.SecurityCode.ToUpperInvariant(), secType)).Add(message);

					if (!message.ShortName.IsEmpty())
						byShortName.SafeAdd(Tuple.Create(message.ShortName.ToUpperInvariant(), secType)).Add(message);

					if (!message.Name.IsEmpty())
						byName.SafeAdd(Tuple.Create(message.Name.ToUpperInvariant(), secType)).Add(message);
				}

				static List<SecurityMessage> TryFind(Dictionary<Tuple<string, SecurityTypes?>, List<SecurityMessage>> dict, Tuple<string, SecurityTypes> key)
				{
					return
						dict.TryGetValue(Tuple.Create(key.Item1, (SecurityTypes?)key.Item2)) ??
						dict.TryGetValue(Tuple.Create(key.Item1, (SecurityTypes?)null));
				}

				foreach (var pair in securities)
				{
					var found = TryFind(byCode, pair.Key) ?? TryFind(byShortName, pair.Key) ?? TryFind(byName, pair.Key);
					MostAppropriated(found, pair.Key, pair.Value);
				}
			}
		}
	}
}