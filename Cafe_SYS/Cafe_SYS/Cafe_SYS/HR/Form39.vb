﻿Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class Form39
    Private Sub Form39_Load(sender As Object, e As EventArgs) Handles Me.Load
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Employee"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        SQL = "Truncate table Table_tmp"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        conn.Open()
        SQL = "Print_Emp_once"
        cmd = New SqlCommand(SQL, conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@code", Label6.Text)
        cmd.Parameters.AddWithValue("@id", Label5.Text)
        cmd.ExecuteNonQuery()
        conn.Close()
        ''
        Form40.Show()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            conn = GetConnect()
            conn.Open()
            SQL = "select * from Employee where Name ='" + ComboBox1.Text + "'"
            cmd = New SqlCommand(SQL, conn)
            dr = cmd.ExecuteReader
            While (dr.Read())
                Label5.Text = dr.GetValue(0).ToString
                Label6.Text = dr.GetValue(1).ToString
            End While
            conn.Close()
        End If
    End Sub
End Class