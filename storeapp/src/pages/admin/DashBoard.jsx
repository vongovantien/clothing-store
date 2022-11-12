import { Grid } from '@mui/material'
import { Container } from '@mui/system'
import { Outlet } from 'react-router-dom'
import CustomizedAccordions from '../../components/AdminNavLeft'


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
