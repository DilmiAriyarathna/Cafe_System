Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form13
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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox1.BackColor = Color.Tan
            CheckBox2.Enabled = False
        Else
            CheckBox2.Enabled = True
            CheckBox1.BackColor = Color.LightGray
        End If
    End Sub
    Public Sub refreshme()
        'Dim form13 As New Form13
        'Me.Hide()
        'form13.Show()
        Me.Refresh()
    End Sub
    Private Sub clr()
        Dim form13 As New Form13
        Me.Hide()
        form13.Show()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox2.BackColor = Color.Tan
        Else
            CheckBox1.Enabled = True
            CheckBox2.BackColor = Color.LightGray
        End If
    End Sub
    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        SQL = "select ID,Name as Supplier_Name,Code as Sup_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
                "Address,Email,regstr_no as Registerd_No,Bank ,accno as Acc_No from Supplier"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub Form13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
        grd()

    End Sub

    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 50  'id
        BunifuCustomDataGrid1.Columns(1).Width = 250    'name
        BunifuCustomDataGrid1.Columns(2).Width = 100    'code
        BunifuCustomDataGrid1.Columns(3).Width = 100    'cnt1
        BunifuCustomDataGrid1.Columns(4).Width = 100    'cnt2
        BunifuCustomDataGrid1.Columns(5).Width = 250    'add
        BunifuCustomDataGrid1.Columns(6).Width = 100    'email
        BunifuCustomDataGrid1.Columns(7).Width = 100    'rgst
        BunifuCustomDataGrid1.Columns(8).Width = 150    'bank
        BunifuCustomDataGrid1.Columns(9).Width = 100    'acc
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If CheckBox1.Checked = True Then
            scr1()
        ElseIf CheckBox2.Checked = True Then
            scr2()
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("Invalid Option !")
            TextBox1.Text = ""
            TextBox1.Focus()
        End If
    End Sub

    Private Sub scr1()
        conn = GetConnect()
        conn.Open()
        SQL = "select ID,Name as Supplier_Name,Code as Sup_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
                "Address,Email,regstr_no as Registerd_No,Bank ,accno as Acc_No from Supplier where " &
                " Name like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub BunifuCustomDataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles BunifuCustomDataGrid1.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            TextBox5.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(1).Value
            TextBox4.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
            TextBox7.Text = BunifuCustomDataGrid1.Rows(e.RowIndex).Cells(2).Value
        End If
        Me.Hide()
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If TextBox6.Text = "1" Then
            Form14.TextBox1.Text = TextBox5.Text
            Form14.TextBox2.Text = TextBox4.Text
            Form14.Label18.Text = TextBox4.Text
            Form14.TextBox3.Focus()
        End If

        ''''''''''''''''''''''''''''''''''''''''''''

        If TextBox6.Text = "2" Then
            Form11.TextBox1.Text = TextBox5.Text
            Form11.TextBox2.Text = TextBox4.Text
            ' Form11.Label18.Text = TextBox4.Text
            Form11.CheckBox1.Focus()
            Form11.CheckBox1.BackColor = Color.Tan
        End If

        '''''''''''''''''''''''''''''''''''''''''''
        If TextBox6.Text = "5" Then
            Form23.TextBox1.Text = TextBox5.Text
            Form23.TextBox2.Text = TextBox4.Text
            ' Form11.Label18.Text = TextBox4.Text
            Form23.ComboBox1.Select()
            Form23.ComboBox1.DroppedDown = True
        End If
        '''''''''''''''''''''''''''''''''''''''''''
        If TextBox6.Text = "3" Then
            Form33.TextBox1.Text = TextBox5.Text
            Form33.TextBox2.Text = TextBox4.Text
            ' Form11.Label18.Text = TextBox4.Text
            Form33.ComboBox5.Select()
            Form33.ComboBox5.DroppedDown = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clr()
    End Sub

    Private Sub scr2()
        conn = GetConnect()
        conn.Open()
        SQL = "select ID,Name as Supplier_Name,Code as Sup_Code,Contact1 as Contact_no1,Contact2 as Contact_no2, " &
                "Address,Email,regstr_no as Registerd_No,Bank ,accno as Acc_No from Supplier where " &
                "  Code like '%" + TextBox1.Text + "%'"
        da = New SqlDataAdapter(SQL, conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
End Class