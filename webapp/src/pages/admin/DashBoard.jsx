import { Grid } from '@mui/material'
import { Container } from '@mui/system'
import React from 'react'
import { Outlet } from 'react-router-dom'
import CustomizedAccordions from '../admin/components/NavLeft'


export const DashBoard = () => {
    return (

        <Grid container spacing={2} style={{ paddingTop: '20px' }}>
            <Grid item xs={3}>
                <Container>
                    <CustomizedAccordions />
                </Container>
            </Grid>
            <Grid item xs={9}>
                <Outlet />
            </Grid>
        </Grid>
    )
}
