import { useEffect, useState } from "react";

const Hello = () => {
  const [data, setData] = useState(null)
  useEffect(() => {
    fetch('job/12345')
      .then((results) => {
        return results.json()
      })
      .then(data => {
          setData(data)
      })
  }, [])

    if (data != null) {
        return (
            <>
                <h1>{data[0].job_title}</h1>
            </>
        )
    }
  
}
export default Hello;