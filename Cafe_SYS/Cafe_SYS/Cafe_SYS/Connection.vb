Imports System.Data
Imports System.Data.SqlClient

Module Connection
    Public conn As New SqlConnection
    Dim strconn As String
    Public SQL As String
    Public cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmdq, cmdw As SqlCommand
    Public da As New SqlDataAdapter
    Public ds As New DataSet
    Public dr, dr1 As SqlDataReader
    Public dt As DataTable
    Public dv As DataView
    Public Function GetConnect()

        If conn.State = ConnectionState.Closed Then
            strconn = "Server =LAPTOP-JJKA3BV5\SQLEXPRESS;" & "initial Catalog = Cafe_SYS;" & " Trusted_Connection=yes"
            conn.ConnectionString = strconn
        ElseIf conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        Return conn
    End Function
End Module
