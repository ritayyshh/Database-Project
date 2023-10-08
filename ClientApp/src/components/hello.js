import { useEffect, useState } from "react";

const Hello = () => {
  const [data, setData] = useState('')
  useEffect(() => {
    const fetchPromise = fetch('Job/Index');
    fetchPromise.then(response => {
      setData(response.data)
    })
  }, [])

  return (
    {data}
  )
}
export default Hello;