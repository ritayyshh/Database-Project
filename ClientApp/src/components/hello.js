import React, {Component} from "react";
import { useEffect, useState } from "react";


const Hello = () => {
  const [data, setData] = useState('')
  useEffect(() => {
    const fetchPromise = fetch('job');
    fetchPromise.then(response => {
      setData(response.data)
    })
  })

  return (
    {data}
  )
}
export default Hello;