Imports System.Data.SqlClient
Imports System.Data
Imports System.OI

Public Class Form18
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer
    Dim user As String

    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Supplier"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub

    Private Sub clr()
        Dim form18 As New Form18
        Me.Hide()
        form18.Show()
    End Sub

    Private Sub src1()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select CAST(Date AS DATE)Date,PO_num, " &
                                "CAST(F_Date AS DATE)F_Date,Supplier,CAST(total as numeric(17,2))Total from PO_vw where Flag = '1' and  " &
                                " Date >= '" + DateTimePicker1.Value + "' and  Date <= '" + DateTimePicker2.Value + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src2()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select CAST(Date AS DATE)Date,PO_num, " &
                                "CAST(F_Date AS DATE)F_Date,Supplier,CAST(total as numeric(17,2))Total from PO_vw where Flag = '1' and  " &
                                "PO_num='" + TextBox1.Text + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src3()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select CAST(Date AS DATE)Date,PO_num, " &
                                "CAST(F_Date AS DATE)F_Date,Supplier,CAST(total as numeric(17,2))Total from PO_vw where Flag = '1' and  " &
                                "Code='" + Label6.Text + "'", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub src4()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select CAST(Date AS DATE)Date,PO_num, " &
                                "CAST(F_Date AS DATE)F_Date,Supplier,CAST(total as numeric(17,2))Total from PO_vw where Flag = '1' and  " &
                                "Code='" + Label6.Text + "' and PO_num='" + TextBox1.Text + "' ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub Form18_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now.Date.AddDays(-(Now.Day) + 1)

        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select CAST(Date AS DATE)Date,PO_num, " &
                                "CAST(F_Date AS DATE)F_Date,Supplier,CAST(total as numeric(17,2))Total from PO_vw where Flag = '1' ", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()

        grd()
        FillCombo()
    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select Code from Supplier where Name ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            Label6.Text = dr.GetValue(0).ToString
        End While
        conn.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = "" And Label6.Text = "" Then
            src1()
        ElseIf Not TextBox1.Text = "" And Label6.Text = "" Then
            src2()
        ElseIf Not Label6.Text = "" And TextBox1.Text = "" Then
            src3()
        ElseIf Not TextBox1.Text = "" And Not Label6.Text = "" Then
            src4()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clr()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()

    End Sub

    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 100
        BunifuCustomDataGrid1.Columns(1).Width = 100
        BunifuCustomDataGrid1.Columns(2).Width = 100
        BunifuCustomDataGrid1.Columns(3).Width = 200
        BunifuCustomDataGrid1.Columns(4).Width = 100
    End Sub
End Class