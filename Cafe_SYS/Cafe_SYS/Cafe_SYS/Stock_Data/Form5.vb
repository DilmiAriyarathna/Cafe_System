Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form5
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

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Display()
        grd()
        count()
        FillCombo()

        ComboBox1.Select()
        ComboBox1.DroppedDown = True

        If (Form1.setname = "") Then
            user = ""
        Else
            user = Form1.setname
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
    Private Sub grd()
        BunifuCustomDataGrid1.Columns(0).Width = 100
        BunifuCustomDataGrid1.Columns(1).Width = 232
    End Sub
    Private Sub Display()
        conn = GetConnect()
        conn.Open()
        da.Fill(dt)
        BunifuCustomDataGrid1.DataSource = dt
        conn.Close()
    End Sub
    Private Sub rfsh()
        Dim form5 As New Form5
        Me.Hide()
        form5.Show()
    End Sub
    Private Sub count()
        conn = GetConnect()
        conn.Open()
        SQL = "select Count(ID) from Item"
        cmd = New SqlCommand(SQL, conn)
        Try
            dr = cmd.ExecuteReader
            While dr.Read
                TextBox6.Text = dr.GetValue(0).ToString
            End While
        Catch ex As Exception
        End Try
        conn.Close()
    End Sub
    Private Sub FillCombo()
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Main_Cat2"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox1.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        '''''''''''''''''''''''''''''''''''''''''''
        conn = GetConnect()
        conn.Open()
        SQL = "select Name from Sub_Cat2"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            ComboBox2.Items.Add(dr.GetString(0))
        End While
        conn.Close()
        ''''''''''''''''''''''''''''''''''''''''''
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
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub
    Private Sub ADD()
        Dim d As DateTime = DateTime.Now
        Dim formata As String = "MM/dd/yyyy"

        Dim t As DateTime = DateTime.Now
        Dim formatb As String = "hh:mm:ss tt"

        conn = GetConnect()
        conn.Open()
        SQL = "insert into Item (ID,Mcat,Scat,Supplier,S_Code,Name,Code,Date,Time,P_Price, " &
                 "R_Price,S_Price,W_Price,Weight,pack_Qty,PR_price,PW_price,spw,wflag,Login,pflag) " &
                    "Values( '" + TextBox6.Text + "' , " &
                           " '" + TextBox15.Text + "' , " &
                           " '" + TextBox16.Text + "' , " &
                           " '" + ComboBox3.Text + "' , '" + TextBox17.Text + "' , " &
                           " '" + TextBox1.Text + "' , '" + TextBox2.Text + "' , " &
                           " '" + d.ToString(formata) + "' , '" + t.ToString(formatb) + "' , " &
                           " '" + TextBox3.Text + "' , '" + TextBox13.Text + "' , '" + TextBox5.Text + "' , '" + TextBox4.Text + "' , " &
                           " '" + TextBox7.Text + "' , '" + TextBox8.Text + "','" + TextBox10.Text + "' , '" + TextBox14.Text + "' , " &
                           " '0' , '0' , '" + user + "' , '0')"

        cmd = New SqlCommand(SQL, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
        MsgBox("Updates Successfuly !")

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If ComboBox1.Text = "" Then
            MsgBox("Invalid Main Category !")
            ComboBox1.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If ComboBox2.Text = "" Then
            MsgBox("Invalid Sub Category !")
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        If ComboBox3.Text = "" Then
            MsgBox("Invalid Supplier !")
            ComboBox2.Focus()
            SendKeys.Send("{F4}")
            Exit Sub
        End If
        ''
        '''''''''''''''''purchase price cannot be empty
        If TextBox3.Text = "" Then
            MsgBox("Invalid Purchase Price !")
            TextBox3.Focus()
            Exit Sub
        End If
        '''''''''''''''''Retail price cannot be empty
        If TextBox13.Text = "" Then
            MsgBox("Invalid Retail Price !")
            TextBox13.Focus()
            Exit Sub
        End If
        '''''''''''''''' Sales price cannot be empty
        If TextBox5.Text = "" Then
            MsgBox("Invalid Sales Price !")
            TextBox5.Focus()
            Exit Sub
        End If
        ''''''''''''''''whaloseles price cannot be empty
        If TextBox4.Text = "" Then
            MsgBox("Invalid WholeSale Price !")
            TextBox4.Focus()
            Exit Sub
        End If
        ''
        Dim i = Convert.ToInt32(TextBox6.Text)
        i += 1
        TextBox6.Text = i.ToString()
        ''''''''''''''''''''''''''''
        ADD()
        Display()
        clr()
    End Sub
    Private Sub clr()
        Dim form5 As New Form5
        Me.Hide()
        form5.Show()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form7.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form71.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form8.Show()
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Main_Cat2 where Name ='" + ComboBox1.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox15.Text = dr.GetValue(1).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Sub_Cat2 where Name ='" + ComboBox2.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox16.Text = dr.GetValue(1).ToString
        End While
        conn.Close()
    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        conn = GetConnect()
        conn.Open()
        SQL = "select * from Supplier where Name ='" + ComboBox3.Text + "'"
        cmd = New SqlCommand(SQL, conn)
        dr = cmd.ExecuteReader
        While (dr.Read())
            TextBox17.Text = dr.GetValue(2).ToString
        End While
        conn.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        '''''''''''''''''first letter capital
        If TextBox1.Text <> "" Then
            Dim a As String = TextBox1.Text
            TextBox1.Text = (StrConv(a, VbStrConv.ProperCase))
            TextBox1.Select(TextBox1.Text.Length, 0)
        End If
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged

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
            Else
                TextBox1.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox12.Text = TextBox1.Text.Substring(0, 2) 'get first two letters

            Dim i = Convert.ToInt32(TextBox6.Text)
            i += 1
            TextBox18.Text = i.ToString()

            TextBox2.Text = TextBox12.Text + "00" + TextBox18.Text
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox13.Focus()
        End If
    End Sub

    Private Sub TextBox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox13.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox5.Focus()
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

    Private Sub TextBox8_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox10.Text = Val(TextBox8.Text) * Val(TextBox13.Text)
            TextBox14.Text = Val(TextBox8.Text) * Val(TextBox4.Text)
            '    TextBox10.Focus()
        End If
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            '  TextBox14.Focus()
        End If
    End Sub

    Private Sub TextBox14_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox14.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            TextBox11.Focus()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        clr()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged

    End Sub
End Class