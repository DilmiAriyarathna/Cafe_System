Imports System.Data.SqlClient

Public Class Form37
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Form37_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.Value = New DateTime(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, 1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Visible = False
    End Sub
End Class