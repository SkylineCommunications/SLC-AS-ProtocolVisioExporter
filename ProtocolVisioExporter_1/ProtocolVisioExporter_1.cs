/*
****************************************************************************
*  Copyright (c) 2023,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

28/02/2023	1.0.0.1		JVT, Skyline	Initial version
****************************************************************************
*/

using System.IO;
using System.IO.Compression;
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Net.Messages;

/// <summary>
/// DataMiner Script Class.
/// </summary>
public class Script
{
	private const string RootPath = @"C:\Skyline DataMiner\Protocols";

	/// <summary>
	/// The Script entry point.
	/// </summary>
	/// <param name="engine">Link with SLAutomation process.</param>
	public void Run(Engine engine)
	{
		var dataminerInfo = (GetDataMinerInfoResponseMessage)engine.SendSLNetSingleResponseMessage(new GetInfoMessage(InfoType.DataMinerInfo));
		var visioArchiveName = $"{dataminerInfo.AgentName}_{dataminerInfo.ID}_Visios.zip";
		var visioArchivePath = Path.Combine(RootPath, visioArchiveName);

		using (var archive = ZipFile.Open(visioArchivePath, ZipArchiveMode.Create))
		{
			foreach (var filePath in Directory.EnumerateFiles(RootPath, "*.vsdx", SearchOption.AllDirectories))
			{
				var folderName = Path.GetDirectoryName(filePath).Replace(RootPath, string.Empty).TrimStart('\\');
				var visioName = Path.Combine(folderName, $"{dataminerInfo.AgentName}_{dataminerInfo.ID}_{Path.GetFileName(filePath)}");
				archive.CreateEntryFromFile(filePath, visioName);
			}
		}
	}
}