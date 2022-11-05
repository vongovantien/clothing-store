import { Accordion, AccordionDetails, AccordionSummary, FormControl, FormControlLabel, Radio, RadioGroup, Typography } from '@mui/material';

import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import React from 'react';

const NavLeft = () => {
    return (
        <>
            <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1a-content"
                    id="panel1a-header"
                >
                    <Typography>Giá</Typography>
                </AccordionSummary>
                <AccordionDetails>

                    <FormControl>
                        <RadioGroup
                            aria-labelledby="demo-radio-buttons-group-label"
                            defaultValue="female"
                            name="radio-buttons-group"
                        >
                            <FormControlLabel value="Dưới 100,000" control={<Radio />} label="Dưới 100,000" />
                            <FormControlLabel value="100,000 - 300,000" control={<Radio />} label="100,000 - 300,000" />
                            <FormControlLabel value="300,000 - 500,000" control={<Radio />} label="300,000 - 500,000" />
                            <FormControlLabel value="500,000 - 800,000" control={<Radio />} label="500,000 - 800,000" />
                            <FormControlLabel value="Trên 800,000" control={<Radio />} label="Trên 800,000" />
                        </RadioGroup>
                    </FormControl>
                </AccordionDetails>
            </Accordion>
            <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel2a-content"
                    id="panel2a-header"
                >
                    <Typography>Màu sắc</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <FormControl>
                        <RadioGroup
                            aria-labelledby="demo-radio-buttons-group-label"
                            defaultValue="female"
                            name="radio-buttons-group"
                        >
                            <FormControlLabel value="Dưới 100,000" control={<Radio />} label="Dưới 100,000" />
                            <FormControlLabel value="100,000 - 300,000" control={<Radio />} label="100,000 - 300,000" />
                            <FormControlLabel value="300,000 - 500,000" control={<Radio />} label="300,000 - 500,000" />
                            <FormControlLabel value="500,000 - 800,000" control={<Radio />} label="500,000 - 800,000" />
                            <FormControlLabel value="Trên 800,000" control={<Radio />} label="Trên 800,000" />
                        </RadioGroup>
                    </FormControl>
                </AccordionDetails>
            </Accordion>
            <Accordion>
                <AccordionSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel2a-content"
                    id="panel2a-header"
                >
                    <Typography>Kích thướt</Typography>
                </AccordionSummary>
                <AccordionDetails>
                    <FormControl>
                        <RadioGroup
                            aria-labelledby="demo-radio-buttons-group-label"
                            defaultValue="female"
                            name="radio-buttons-group"
                        >
                            <FormControlLabel value="Dưới 100,000" control={<Radio />} label="M" />
                            <FormControlLabel value="100,000 - 300,000" control={<Radio />} label="L" />
                            <FormControlLabel value="300,000 - 500,000" control={<Radio />} label="XL" />
                            <FormControlLabel value="500,000 - 800,000" control={<Radio />} label="XXL" />
                            <FormControlLabel value="Trên 800,000" control={<Radio />} label="XXXL" />
                        </RadioGroup>
                    </FormControl>
                </AccordionDetails>
            </Accordion>
        </>

    )
}

export default NavLeft
