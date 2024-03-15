Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Public Class Form4
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
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Select()
        ComboBox1.DroppedDown = True

        fillcombo()
        '''''''''''''''
        Displaydata()
        grd()
        ''''
        ComboBox1.Focus()
        SendKeys.Send("{F4}")
        ''
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT count(ID) FROM Products"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception
        End Try
        conn.Close()

        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If

    End Sub
    Private Sub fillcombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Sub_Cat"
        cmd1 = New SqlCommand(SQL, conn)
        dr = cmd1.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Supplier"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox3.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 85
        BunifuCustomDataGrid1.Columns(1).Width = 250
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ''''''
        If ComboBox1.Text = "" Then
            MsgBox("Invalid Main Category !")
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''''
        If ComboBox2.Text = "" Then
            MsgBox("Invalid Sub Category !")
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''''
        If TextBox10.Text = "" Then
            MsgBox("Invalid Supplier !")
            TextBox10.Focus()
            ' SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''''''
        If TextBox3.Text = "" Then
            MsgBox("Invalid Name !")
            TextBox3.Focus()
            Exit Sub
        End If
        ''''''
        If TextBox2.Text = "" Then
            MsgBox("Invalid Code !")
            TextBox2.Focus()
            Exit Sub
        End If
        ''''''
        If TextBox4.Text = "" Then
            MsgBox("Invalid Purchase Price !")
            TextBox4.Focus()
            Exit Sub
        End If
        ''''''
        If TextBox7.Text = "" Then
            MsgBox("Invalid Retail Price !")
            TextBox7.Focus()
            Exit Sub
        End If
        ''''''
        If TextBox8.Text = "" Then
            MsgBox("Invalid Sales Price !")
            TextBox8.Focus()
            Exit Sub
        End If
        '''''
        add()
        MsgBox("Successfully Update !")
        Displaydata()
        Clr()
    End Sub
    Private Sub add()
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"

        conn = GetConnect()
        conn.Open()
        SQL = " insert into Products " +
        "( ID,Mcat,Scat,Supplier,S_Code,Name,Code,Date,Time,P_Price,R_Price,S_Price,Discrip,Login,pflag ) " +
        "values ( " +
        "  '" + TextBox1.Text + "' , '" + ComboBox1.Text + "' , '" + ComboBox2.Text + "' , '" + ComboBox3.Text + "' , " +
        " '" + TextBox10.Text + "' , '" + TextBox3.Text + "' , '" + TextBox2.Text + "' , '" + d.ToString(formata) + "' , " +
        " '" + t.ToString(formatb) + "' , '" + TextBox4.Text + "' , '" + TextBox7.Text + "' , '" + TextBox8.Text + "' , " +
        " '" + TextBox5.Text + "' , '" + user + "' , '1' )"
        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub
    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox1.Text = i.ToString()
    End Sub
    Private Sub Clr()
        Dim form4 As New Form4
        Me.Hide()
        form4.Show()
    End Sub
    Public Sub Displaydata()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Code,Name from Products", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Clr()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox3.Focus()
            SendKeys.Send("{F4}")
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            CheckBox1.Focus()
            CheckBox1.BackColor = Color.Tan
        End If
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            If CheckBox1.Checked = True Then
                TextBox2.Focus()
                TextBox2.Enabled = True
            Else
                TextBox3.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox9.Text = TextBox3.Text.Substring(0, 2) 'get first two letters

            TextBox2.Text = TextBox9.Text + "/" + "00" + TextBox1.Text
            TextBox5.Focus()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text <> "" Then
            Dim a As String = TextBox3.Text
            TextBox3.Text = (StrConv(a, VbStrConv.ProperCase))
            TextBox3.Select(TextBox3.Text.Length, 0)
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox4.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox7.Focus()
        End If
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox8.Focus()
        End If
    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Supplier where Name ='" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox10.Text = dr.GetValue(2).ToString
        End While
        conn.Close()
    End Sub

    Private Sub Label8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label8.Click

    End Sub
End Class