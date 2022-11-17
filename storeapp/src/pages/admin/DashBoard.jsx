import { Grid } from '@mui/material';
import { Container } from '@mui/system';
import { CustomizedAccordions } from 'components/AdminNavLeft';
import { Outlet } from 'react-router-dom';


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
