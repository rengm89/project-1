Imports System.Data.OleDb
Module DatosGlobales
    Public Comando As New OleDbCommand
    Public Conexion As New OleDbConnection
    Public Adaptador As New OleDbDataAdapter

    Public DR As OleDbDataReader
    Public DS As DataSet

    Public CadenaDeConexion As String = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=Libreria.mdb"


End Module
