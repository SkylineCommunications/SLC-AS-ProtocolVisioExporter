
# Protocol Visio Exporter

This script can be used to export all Visio files linked to a protocol from a DMS.

When executing the script, it will scan through all folders in the C:\Skyline DataMiner\Protocols and create an archive (zip) stored in the C:\Skyline DataMiner\Protocols directory.
The name of the archive will have the following format: [AgentName]_[AgentId]_Visios.zip.
Each Visio in the archive will be stored in a dedicated folder with the name of the linked protocol and the name of the Visio will have the following format: [AgentName]_[AgentId]_[VisioName].zip
