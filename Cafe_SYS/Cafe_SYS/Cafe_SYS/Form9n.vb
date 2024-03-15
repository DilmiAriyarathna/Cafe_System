Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form9n
    Public conn As New SqlConnection
    Dim SQL As String
    Dim cmd, cmd1, cmd2 As SqlCommand
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim dv As DataView
    Dim x As Integer

    Private Sub Form9n_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = GetConnect()
        conn.Open()
        SQL = "SELECT count(ID) FROM Form_List"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString()
            End While
        Catch ex As Exception
        End Try
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        ComboBox1.Select()
        ComboBox1.DroppedDown = True
        '''''''''''''''''''''''''''''
        Displaydata()
        grd()
        Fillcombo()
    End Sub

    Public Sub Displaydata()
        conn = GetConnect()
        conn.Open()
        da = New SqlDataAdapter("select Name as Form, Caption as Caption from Form_List", conn)
        dt = New DataTable()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 145
        BunifuCustomDataGrid1.Columns(1).Width = 300

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox2.Focus()
            ComboBox2.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            ComboBox3.Focus()
            ComboBox3.DroppedDown = True
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then


            If (ComboBox3.Text = "Reports") Then
                ComboBox4.Enabled = True
                ComboBox4.Focus()
                ComboBox4.DroppedDown = True
            Else
                TextBox1.Focus()
            End If

        End If
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox3.Focus()
            Label10.Text = Label9.Text + TextBox2.Text
        End If
    End Sub
    Private Sub refrsh()
        Dim form9n As New Form9n
        Me.Hide()
        form9n.Show()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()
        ''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Form_List where  order_id = '" + TextBox3.Text + "'"
        cmd1 = New SqlCommand(SQL, conn)
        dt = New DataTable
        da = New SqlDataAdapter(cmd1)
        da.Fill(dt)
        x = Convert.ToInt32(dt.Rows.Count.ToString())
        If x > 0 Then
            MsgBox("Already Exist Order ID !")
            TextBox3.Text = ""
            TextBox3.Focus()
            Exit Sub
        Else
            insert()
        End If
        conn.Close()


    End Sub
    Private Sub insert()
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"
        ''''
        Try
            conn = GetConnect()
            conn.Open()
            SQL = "insert into Form_List(ID,Caption,Name,Category,S_cat,Form_id,order_id,usert,project,flag,date,time) " &
                    " values ( " &
                    " '" + TextBox6.Text + "' , " &
                    " '" + TextBox1.Text + "' , " &
                    " '" + Label10.Text + "' , " &
                    " '" + ComboBox3.Text + "' , " &
                    " '" + ComboBox4.Text + "' , " &
                    " '" + TextBox2.Text + "' , " &
                    " '" + TextBox3.Text + "' , " &
                    " '" + ComboBox1.Text + "' , " &
                    " '" + ComboBox2.Text + "' , '1' ," &
                    " '" + d.ToString(formata) + "' , '" + t.ToString(formatb) + "') "
            cmd = New SqlCommand(SQL, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Form Saved !")
            conn.Close()
            Clr()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Clr()
        Dim form9n As New Form9n
        Me.Hide()
        form9n.Show()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            Button1.Focus()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form91n.Show()
    End Sub

    Private Sub Fillcombo()

        conn = GetConnect()
        conn.Open()
        SQL = "select UName from Usert"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select MainCat from Form_Mcat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox3.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select SubCat from Form_Scat"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox4.Items.Add(dr.GetString(0))
        End While
        conn.Close()
    End Sub


End Class