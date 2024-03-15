Imports System.Data.SqlClient

Public Class Form36
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn = GetConnect()
        conn.Open()
        SQL = "insert into GRN_Filter Select * From grn_report"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()


    End Sub
End Class