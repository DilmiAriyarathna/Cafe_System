Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form16
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

    Private Sub Form16_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 100
        BunifuCustomDataGrid1.Columns(1).Width = 100
        BunifuCustomDataGrid1.Columns(2).Width = 100
        BunifuCustomDataGrid1.Columns(3).Width = 200
        BunifuCustomDataGrid1.Columns(4).Width = 100
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        clr()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox2.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            TextBox3.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            '//  TextBox4.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
        End If
        Me.Hide()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox6.Text = "1" Then
            Form11.TextBox13.Text = TextBox2.Text
            Form11.TextBox13.Focus()
            Form11.Button12.Enabled = True
            Form11.Button11.Enabled = False
            Form11.Button10.Enabled = False
        End If
    End Sub

    Private Sub clr()
        Dim form16 As New Form16
        Me.Hide()
        form16.Show()
    End Sub
End Class