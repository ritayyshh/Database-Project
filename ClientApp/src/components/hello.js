import axios from 'axios';
import React, { useEffect, useState } from "react";
import {
    Accordion,
    AccordionBody,
    AccordionHeader,
    AccordionItem,
} from 'reactstrap';
const Hello = () => {
    const [open, setOpen] = useState('1');
    const toggle = (id) => {
        if (open === id) {
            setOpen();
        } else {
            setOpen(id);
        }
    };
  const [data, setData] = useState(null)
  useEffect(() => {
      axios.get('/job').then(res => {
          setData(res.data);
      })
  }, [])

    if (data != null) {
        return (
            <div>
                <Accordion open={open} toggle={toggle}>
                    {data.map((d, i) => {
                        return (
                            <AccordionItem key={i}>
                                <AccordionHeader targetId={`${i}`}>{d.job_title}</AccordionHeader>
                                <AccordionBody accordionId={`${i}`}>
                                    {d.job_description}
                                </AccordionBody>
                            </AccordionItem >
                        );
                    })}
                </Accordion>
            </div>
        )
    }
  
}
export default Hello;