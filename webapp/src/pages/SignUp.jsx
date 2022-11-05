import { Container, FormControl, TextField } from '@mui/material'
import React from 'react'

export const SignUp = () => {
    return (
        <Container style={{ paddingTop: "20px" }} maxWidth="sm">
            <h1 style={{ textAlign: "center" }}>Đăng ký</h1>
            <FormControl style={{ justifyContent: "center" }} fullWidth variant="outlined">
                <TextField style={{ marginTop: "20px" }}
                    id="outlined-password-input"
                    label="Username"
                    type="text"
                    autoComplete="current-password"
                />
                <TextField style={{ marginTop: "20px" }}
                    id="outlined-password-input"
                    label="Password"
                    type="password"
                    autoComplete="current-password"

                />
                <TextField style={{ marginTop: "20px" }}
                    id="outlined-password-input"
                    label="Password"
                    type="password"
                    autoComplete="current-password"

                />
            </FormControl>

        </Container>
    )
}
